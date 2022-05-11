using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ExamWebEF;
namespace OldExam
{
    /// <summary>
    /// 与数据库进行操作的类型
    /// </summary>
    public class DBOp
    {
        //查找所有学生

        ///班级
        #region
        //添加班级
        public async Task<IResult> DBPostClass(UserClassDTO userClassDTO, ExamContext db)
        {
            var className = userClassDTO?.ClassName;
            if (DBSelectClass(className, db).Result != null)
                return Results.Conflict("object Existed");
            else
            {
                var newClass = new Class { Name = className };
                db.Add(newClass);
            }
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }

        //修改班级
        public async Task<IResult> DBPutClass(string oldClassName, UserClassDTO userClassDTO, ExamContext db)
        {
            var className = userClassDTO?.ClassName;
            if (className == null)
                return Results.BadRequest("Need ClassName");
            var oldClass = DBSelectClass(oldClassName, db).Result;
            if (oldClass == null)
                return Results.Conflict("object not exist");
            else
                oldClass.Name = className;
            db.Update(oldClass);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }

        //查询班级
        public async Task<IResult> DBGetClass(string className, ExamContext db)
        {
            //查询所有的班级
            if (className == null)
            {
                var classes = from AClass in db.classes
                              select AClass;
                return Results.Ok(classes.ToList());
            }
            var getClassName = DBSelectClass(className, db);
            if (getClassName.Result == null)
                return Results.BadRequest("Class NOT EXIST");
            return Results.Ok(getClassName.Result.Name);
        }

        //删除班级
        public async Task<IResult> DBDeleteClass(string className, ExamContext db)
        {
            var delClass = DBSelectClass(className, db).Result;
            if (delClass == null)
                Results.Ok("Object Not Exist");
            else
                db.Remove(delClass);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }

        #endregion
        ///用户班级
        #region
        //为用户添加班级
        public async Task<IResult> DBUserAddUserClass(string account, UserClassDTO userClassDTO, ExamContext db)
        {
            userClassDTO.Account = account;
            var accountClass = userClassDTO?.ClassName;
            var AccountClass = DBSelectClass(accountClass, db).Result;
            if (AccountClass == null)
            {
                Class stuclass = new Class
                {
                    Name = accountClass,
                };
                db.Add(stuclass);
                db.SaveChanges();
            }
            AccountClass = DBSelectClass(accountClass, db).Result;
            var Account = DBSelectUserInfo(account, db);
            if (Account == null)
                return Results.BadRequest("User Not Existed");
            if (Account.GetType() == typeof(UserStudent))
            {
                int id = (int)Account.Id;
                var studentClass = (from UserStudentclass in db.userStudentClasses
                                    where UserStudentclass.UserStudentId == id
                                    select UserStudentclass).SingleOrDefault();
                if (studentClass != null)
                    return Results.BadRequest("Student's Class Existed");
                var stuClass = new UserStudentClass { userStudent = Account, UserStudentId = Account.Id, classes = AccountClass, ClassId = AccountClass.Id };
                db.Add(stuClass);
            }
            else if (Account.GetType() == typeof(UserTeacher))
            {
                int id = (int)Account.Id;
                var teacherclass = (from TClass in db.userTeacherClasses
                                    where TClass.UserTeacherId == id
                                    select TClass.ClassId).ToList();
                if (teacherclass.Contains(AccountClass.Id))
                    return Results.BadRequest("Teacher's Class Existed");
                var teacherClass = new UserTeacherClass { UserTeacher = Account, UserTeacherId = Account.Id, classes = AccountClass, ClassId = AccountClass.Id };
                db.Add(teacherClass);
            }
            else
            {
                return Results.BadRequest();
            }
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }

        //获取用户的班级
        public async Task<IResult> DBUserGetUserClass(string account, ExamContext db)
        {
            var Account = DBSelectUserInfo(account, db);
            if (Account.GetType() == typeof(UserStudent))
            {
                int id = (int)Account.Id;
                var userClassName = (from studentClass in db.userStudentClasses
                                     join stuclass in db.classes on studentClass.ClassId equals stuclass.Id
                                     where studentClass.UserStudentId == id
                                     select stuclass.Name).SingleOrDefault();
                return Results.Ok(userClassName);
            }
            else if (Account.GetType() == typeof(UserTeacher))
            {
                int id = (int)Account.Id;
                var userClassName = (from teacherClass in db.userTeacherClasses
                                     join teacherclass in db.classes on teacherClass.ClassId equals teacherclass.Id
                                     where teacherClass.UserTeacherId == id
                                     select teacherclass.Name).ToList();
                return Results.Ok(userClassName);
            }
            else
            {
                return Results.BadRequest("User Not Existed");
            }
        }

        //修改用户的班级
        public async Task<IResult> DBUserPutUserClass(String account, UserClassDTO userClassDTO, ExamContext db)
        {
            userClassDTO.Account = account;
            var Account = DBSelectUserInfo(account, db);
            if (Account == null)
            {
                return Results.BadRequest("Need Account");
            }
            var userClass = DBSelectClass(userClassDTO.ClassName, db).Result;
            if (userClass == null)
                return Results.BadRequest("Need Existed Classes");
            if (Account.GetType() == typeof(UserStudent))
            {
                var id = (int)Account.Id;
                var userStuClass = (from UserStudentClass in db.userStudentClasses
                                    where UserStudentClass.UserStudentId == id
                                    select UserStudentClass).SingleOrDefault();
                if (userStuClass == null)
                {
                    await DBUserAddUserClass(account, userClassDTO, db);
                    return Results.Ok("Add Success");
                }
                userStuClass.classes = userClass;
                userStuClass.ClassId = userClass.Id;
                db.Update(userStuClass);
                await db.SaveChangesAsync();
            }
            else if (Account.GetType() == typeof(UserTeacher))
            {
                var id = (int)Account.Id;
                var userTeacherClass = (from UserTeacherClass in db.userTeacherClasses
                                        where UserTeacherClass.UserTeacherId == id && UserTeacherClass.classes.Name == userClass.Name
                                        select UserTeacherClass).ToList();
                if (userTeacherClass.Count == 0)
                {
                    await DBUserAddUserClass(account, userClassDTO, db);
                    return Results.Ok("Add Success");
                }
                else
                {
                    return Results.BadRequest("object existed");
                }
            }
            else
            {
                return Results.BadRequest();
            }
            return Results.Ok("Change success");
        }

        //删除用户的班级
        public async Task<IResult> DBDeleteUserClass(string account, string className, ExamContext db)
        {
            var Account = DBSelectUserInfo(account, db);
            int id = (int)Account.Id;
            if (Account == null)
                return Results.BadRequest("User NOT EXISTS");
            if (Account.GetType() == typeof(UserStudent))
            {
                var stuClass = (from UserStudentClass in db.userStudentClasses
                                where UserStudentClass.UserStudentId == id
                                select UserStudentClass).SingleOrDefault();
                db.Remove(stuClass);
                await db.SaveChangesAsync();
            }
            else if (Account.GetType() == typeof(UserTeacher))
            {
                var userTeacherClass = (from UserTeacherClass in db.userTeacherClasses
                                        where UserTeacherClass.UserTeacherId == id && UserTeacherClass.classes.Name == className
                                        select UserTeacherClass);
                db.RemoveRange(userTeacherClass);
                await db.SaveChangesAsync();
            }
            else
            {
                return Results.BadRequest();
            }
            return Results.Ok("success");
        }


        public async Task<Class> DBSelectClass(string className, ExamContext db)
        {
            var classes = from c in db.classes
                          where c.Name == className
                          select c;
            return await classes.SingleOrDefaultAsync();
        }
        #endregion
        ///用户
        #region
        /// <summary>
        /// 插入用户信息的方法
        /// </summary>
        /// <param name="userDTO">前端用户DTO</param>
        /// <param name="db">数据库</param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task<IResult> DBInsertUser(UserDTO userDTO, ExamContext db)
        {
            using (var context = db)
            {

                if ((DBSelectUserInfo(userDTO.userStudent.Account, context) == null) && userDTO.userStudent.Account != null)
                {
                    var stu = userDTO.userStudent;
                    context.Add(stu);
                    await context.SaveChangesAsync();
                }


                else if ((DBSelectUserInfo(userDTO.userTeacher.Account, context) == null) && userDTO.userTeacher.Account != null)
                {
                    var teacher = userDTO.userTeacher;
                    db.Add(teacher);
                    await context.SaveChangesAsync();
                }
                else
                {
                    return Results.BadRequest("Request Wrong");
                }
            }
            return Results.Ok("success");
        }
        public async Task<IResult> DBLogin(string account, string password, ExamContext db)
        {
            var userAccount = account;
            var user = new UserDTO();
            var userstu = (from student in db.userStudents
                           where student.Account == userAccount && student.Password == password
                           select student).SingleOrDefault();
            if (userstu != null)
            {
                user.userStudent = userstu;
                user.role = "student";
                return Results.Ok(user);
            }
            var userteacher = (from teacher in db.userTeachers
                               where teacher.Account == userAccount && teacher.Password == password
                               select teacher).SingleOrDefault();
            if (userteacher != null)
            {
                user.userTeacher = userteacher;
                user.role = "teacher";
                return Results.Ok(user);
            }
            return Results.BadRequest();
        }


        /// <summary>
        /// 根据用户账户查询用户信息方法
        /// </summary>
        /// <param name="account">用户的账户</param>
        /// <param name="db">数据库</param>
        /// <returns>用户信息</returns>
        public async Task<IResult> DBFindUserInfo(string account, ExamContext db)
        {
            if (account == "%STU%")
            {
                var stu = from Stu in db.userStudents
                          select Stu;
                return Results.Ok(stu.ToList());
            }
            if (account == "%TEACHER%")
            {
                var Tuser = from User in db.userTeachers
                            select User;
                return Results.Ok(Tuser.ToList());
            }
            var user = DBSelectUserInfo(account, db);
            if (user == null)
            {
                return Results.NotFound("No Exist");
            }
            else
            {
                return Results.Ok(user);
            }

        }

        public UserStudent SDBFindUserInfo(string account, ExamContext db)
        {
            var user = db.userStudents.Where(s => s.Account.Equals(account)).FirstOrDefault();
            return user;
        }

        /// <summary>
        /// 更新用户信息
        /// </summary>
        /// <param name="account"></param>
        /// <param name="userDTO"></param>
        /// <param name="db"></param>
        /// <returns></returns>
        public async Task<IResult> DBUpdateUserInfo(string account, UserDTO userDTO, ExamContext db)
        {
            ///查询用户是否存在
            dynamic user = DBSelectUserInfo(account, db);
            //通过反射获取需要修改的信息并更新到数据库
            Dictionary<string, object> properties = new Dictionary<string, object>();
            if (user == null)
                return Results.NotFound("No Exist");
            else
            {
                //读取需要修改的项

                if (userDTO.userStudent != null)
                {
                    foreach (PropertyInfo propertyInfoStu in userDTO.userStudent.GetType().GetProperties())
                    {
                        if (propertyInfoStu.GetValue(userDTO.userStudent, null) != null)
                            properties.Add(propertyInfoStu.Name, propertyInfoStu.GetValue(userDTO.userStudent));
                        else
                            continue;
                    }
                }
                else if (userDTO.userTeacher != null)
                {
                    foreach (PropertyInfo propertyInfoTeacher in userDTO.userTeacher.GetType().GetProperties())
                    {
                        if (propertyInfoTeacher.GetValue(userDTO.userTeacher, null) != null)
                            properties.Add(propertyInfoTeacher.Name, propertyInfoTeacher.GetValue(userDTO.userTeacher));
                        else
                            continue;
                    }
                }
            }
            properties.Remove("Id");
            //更新需要修改的项
            foreach (var key in properties.Keys)
            {
                foreach (PropertyInfo propertyInfo in user.GetType().GetProperties())
                {
                    if (key == propertyInfo.Name)
                        propertyInfo.SetValue(user, properties[key]);
                }
            }
            db.Update(user);
            var count = await db.SaveChangesAsync();
            if (count > 0)
                return Results.Ok("success");
            else
                return Results.Ok("No Change");

        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task<IResult> DBDeleteUserInfo(string account, ExamContext context)
        {
            var user = DBSelectUserInfo(account, context);
            if (user == null)
                return Results.NotFound("No Exist");
            else
            {
                context.Remove(user);
                await context.SaveChangesAsync();
                return Results.Ok("success");
            }
        }


        /// <summary>
        /// 搜索用户是否存在并返回用户
        /// </summary>
        /// <param name="account"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public dynamic DBSelectUserInfo(string account, ExamContext context)
        {
            var userAccount = account;
            var userstu = (from student in context.userStudents
                           where student.Account == userAccount
                           select student).SingleOrDefault();
            var userteacher = (from teacher in context.userTeachers
                               where teacher.Account == userAccount
                               select teacher).SingleOrDefault();
            dynamic user = (userstu == null) ? userteacher : userstu;
            return user;
        }
        #endregion
        ///题库
        #region
        public async Task<IResult> InsertQuestion(QuestionDTO questionDTO, ExamContext db)
        {
            if (questionDTO.QuestionClass == "choice")
            {
                QuestionDTO choiceDTO = questionDTO;
                var choice = new QuestionContext
                {
                    Stem = choiceDTO.Stem,
                    Cost = choiceDTO.Cost,
                    DifficultyLevel = choiceDTO.DifficutlyLevels,
                    Subject = choiceDTO.Subject,
                    questionChoice = new QuestionChoice
                    {
                        ChoiceA = choiceDTO.ChoiceOptions.ChoiceA,
                        ChoiceB = choiceDTO.ChoiceOptions.ChoiceB,
                        ChoiceC = choiceDTO.ChoiceOptions.ChoiceC,
                        ChoiceD = choiceDTO.ChoiceOptions.ChoiceD
                    },
                    answer = new Answer
                    {
                        ChoiceAnswer = choiceDTO.ChoiceAnswer
                    }
                };
                await db.AddAsync(choice);
            }
            else if (questionDTO.QuestionClass == "txt")
            {
                QuestionDTO txtDTO = questionDTO;
                var txt = new QuestionContext
                {
                    Stem = txtDTO.Stem,
                    Cost = txtDTO.Cost,
                    DifficultyLevel = txtDTO.DifficutlyLevels,
                    Subject = txtDTO.Subject,
                    answer = new Answer
                    {
                        TxtAnswer = txtDTO.TxtAnswer
                    }
                };
                await db.AddAsync(txt);
            }
            else
            {
                return Results.BadRequest("Class Error");
            }
            await db.SaveChangesAsync();
            return Results.Ok("Success");
        }
        public async Task<IResult> SeletQuestion(QuestionDTO questionDTO, ExamContext db)
        {
            ResultQuestionDTO ResultQuestionDTO = new ResultQuestionDTO();
            ResultQuestionDTO.questionDTOs = new List<QuestionDTO>();
            var answer = db.QuestionContext.
                Where(x => questionDTO.Id == 0 ? x.Id != null : x.Id == questionDTO.Id).
                Where(x => questionDTO.Stem == null ? x.Id != null : x.Stem.Contains(questionDTO.Stem)).
                Where(x => questionDTO.DifficutlyLevels == 0 ? x.DifficultyLevel != null : x.DifficultyLevel == questionDTO.DifficutlyLevels).
                Where(x => questionDTO.Subject == null ? x.Subject != null : x.Subject == questionDTO.Subject).
                Where(x => questionDTO.Cost == 0 ? x.Cost != null : x.Cost == questionDTO.Cost).
                Where(x => questionDTO.QuestionClass == null ? true : (questionDTO.QuestionClass == "txt" ? x.QuestionChoiceId == null : x.QuestionChoiceId != null)).Include(x => x.questionChoice).Include(x => x.answer);
            var answer2 = answer.
                Where(x => questionDTO.ChoiceAnswer == 0 ? true : x.answer.ChoiceAnswer == questionDTO.ChoiceAnswer).
                Where(x => questionDTO.ChoiceOptions.ChoiceA == null ? (true) : x.questionChoice.ChoiceA.Contains(questionDTO.ChoiceOptions.ChoiceA)).
                Where(x => questionDTO.ChoiceOptions.ChoiceB == null ? (true) : x.questionChoice.ChoiceB.Contains(questionDTO.ChoiceOptions.ChoiceB)).
                Where(x => questionDTO.ChoiceOptions.ChoiceC == null ? (true) : x.questionChoice.ChoiceC.Contains(questionDTO.ChoiceOptions.ChoiceC)).
                Where(x => questionDTO.ChoiceOptions.ChoiceD == null ? (true) : x.questionChoice.ChoiceD.Contains(questionDTO.ChoiceOptions.ChoiceD)).
                Where(x => questionDTO.TxtAnswer == null ? true : x.answer.TxtAnswer.Contains(questionDTO.TxtAnswer))
                .Include(x => x.questionChoice).Include(x => x.answer);
            if (answer2.FirstOrDefault() == null)
                return Results.Ok("None");
            foreach (var result in answer2)
                GetQuestion(result, ResultQuestionDTO);
            await db.DisposeAsync();
            return Results.Ok(ResultQuestionDTO);
        }
        public async Task<IResult> UpdateQuestion(int Id, QuestionDTO questionDTO, ExamContext db)
        {
            var question = (from i in db.QuestionContext
                            where i.Id == Id
                            select i).Include(x => x.answer).Include(x => x.questionChoice).SingleOrDefault();
            question.Id = (questionDTO.Id == 0 ? question.Id : (int)questionDTO.Id);
            question.Stem = (questionDTO.Stem == null ? question.Stem : questionDTO.Stem);
            question.Subject = (questionDTO.Subject == null ? question.Subject : questionDTO.Subject);
            question.Cost = (questionDTO.Cost == 0 ? question.Cost : questionDTO.Cost);
            question.DifficultyLevel = (questionDTO.DifficutlyLevels == 0 ? question.DifficultyLevel : questionDTO.DifficutlyLevels);
            question.answer.ChoiceAnswer = (questionDTO.ChoiceAnswer == 0 ? question.answer.ChoiceAnswer : questionDTO.ChoiceAnswer);
            question.answer.TxtAnswer = (questionDTO.TxtAnswer == null ? question.answer.TxtAnswer : questionDTO.TxtAnswer);
            if (questionDTO.ChoiceOptions != null)
            {
                question.questionChoice.ChoiceA = (questionDTO.ChoiceOptions.ChoiceA == null ? question.questionChoice.ChoiceA : questionDTO.ChoiceOptions.ChoiceA);
                question.questionChoice.ChoiceB = (questionDTO.ChoiceOptions.ChoiceB == null ? question.questionChoice.ChoiceB : questionDTO.ChoiceOptions.ChoiceB);
                question.questionChoice.ChoiceC = (questionDTO.ChoiceOptions.ChoiceC == null ? question.questionChoice.ChoiceC : questionDTO.ChoiceOptions.ChoiceC);
                question.questionChoice.ChoiceD = (questionDTO.ChoiceOptions.ChoiceD == null ? question.questionChoice.ChoiceD : questionDTO.ChoiceOptions.ChoiceD);
            }
            db.Update(question);
            await db.SaveChangesAsync();
            return Results.Ok();
        }
        public async Task<IResult> DeleteQuestion(int Id, ExamContext db)
        {
            var question = (from i in db.QuestionContext
                            where i.Id == Id
                            select i).Include(x => x.answer).Include(x => x.questionChoice).SingleOrDefault();
            if (question == null)
                return Results.NotFound("Object not Existed");
            db.Remove(question);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public ResultQuestionDTO GetQuestion(QuestionContext questionContext, ResultQuestionDTO result)
        {
            if (questionContext.QuestionChoiceId != null)
            {
                result.questionDTOs.Add(new QuestionDTO
                {
                    Id = questionContext.Id,
                    Stem = questionContext.Stem,
                    DifficutlyLevels = questionContext.DifficultyLevel,
                    Subject = questionContext.Subject,
                    Cost = questionContext.Cost,
                    QuestionClass = "choice",
                    ChoiceOptions = new ChoiceOption
                    {
                        ChoiceA = questionContext.questionChoice.ChoiceA,
                        ChoiceB = questionContext.questionChoice.ChoiceB,
                        ChoiceC = questionContext.questionChoice.ChoiceC,
                        ChoiceD = questionContext.questionChoice.ChoiceD
                    },
                    ChoiceAnswer = questionContext.answer.ChoiceAnswer,
                });
            }
            else
            {
                result.questionDTOs.Add(new QuestionDTO
                {
                    Id = questionContext.Id,
                    Stem = questionContext.Stem,
                    DifficutlyLevels = questionContext.DifficultyLevel,
                    Subject = questionContext.Subject,
                    Cost = questionContext.Cost,
                    QuestionClass = "txt",
                    TxtAnswer = questionContext.answer.TxtAnswer
                });
            }
            return result;
        }
        #endregion
        ///试卷
        #region
        public async Task<IResult> AddExam(string grade,ExamDTO examDTO, ExamContext db)
        {
            if (examDTO == null)
                return Results.BadRequest("Need Body");
            Exam exam = new Exam()
            {
                Name = examDTO.ExamName,
                Date = Convert.ToDateTime(examDTO.ExamDate),
                Time = Convert.ToInt32(examDTO.TimeLong),
                UserTeacherId = Convert.ToInt32(examDTO.Teacher),
                userTeacher = (from i in db.userTeachers where i.Id == examDTO.Teacher select i).SingleOrDefault(),
                Score = Convert.ToInt32(examDTO.ExamScore),
            };
            db.Add(exam);
            await db.SaveChangesAsync();
            var student = (from i in db.classes
                          join j in db.userStudentClasses on i.Id equals j.ClassId
                          where i.Name.Contains(grade)
                          join k in db.userStudents on j.UserStudentId equals k.Id
                          select k).ToList();
            foreach(var stu in student)
            {
                var studentexam = new StudentExam
                {
                    UserStudentId = stu.Id,
                    userStudent = stu,
                    exam = exam,
                    ExamId = exam.Id
                };
                db.Add(studentexam);
                await db.SaveChangesAsync();
            }
            return Results.Ok("success");
        }
        public async Task<IResult> GetExam(ExamDTO examDTO, ExamContext db)
        {
            var result = new ResultExamDTO();
            result.exams = new List<ExamDTO>();
            var exam = db.exams.
                Where(x => examDTO.Id == 0 ? true : x.Id == examDTO.Id).
                Where(x => examDTO.ExamName == null ? true : x.Name == examDTO.ExamName).
                Where(x => examDTO.ExamDate == null ? true : x.Date.Equals(Convert.ToDateTime(examDTO.ExamDate))).
                Where(x => examDTO.TimeLong == 0 ? true : x.Time == examDTO.TimeLong).
                Where(x => examDTO.Teacher == 0 ? true : x.UserTeacherId == examDTO.Teacher).
                Where(x => examDTO.ExamScore == 0 ? true : x.Score == examDTO.ExamScore)
                .Include(x => x.userTeacher).Include(x => x.questions);
            foreach (var e in exam)
            {
                result.exams.Add(new ExamDTO
                {
                    Id = e.Id,
                    ExamName = e.Name,
                    ExamDate = e.Date.ToString(),
                    TimeLong = e.Time,
                    Teacher = e.UserTeacherId,
                    ExamScore = e.Score
                });
            }
            return Results.Ok(result);
        }
        public async Task<IResult> UpdateExam(ExamDTO examDTO, ExamContext db)
        {
            var exam = (from i in db.exams
                        where i.Id == examDTO.Id
                        select i).SingleOrDefault();
            if (exam == null)
                return Results.NotFound("Object Not Existed");
            exam.Date = examDTO.ExamDate == null ? exam.Date : Convert.ToDateTime(examDTO.ExamDate);
            exam.Time = examDTO.TimeLong == null ? exam.Time : (float)examDTO.TimeLong;
            exam.Score = examDTO.ExamScore == null ? exam.Score : (int)examDTO.ExamScore;
            exam.UserTeacherId = examDTO.Teacher == null ? exam.UserTeacherId : (int)examDTO.Teacher;
            exam.Name = examDTO.ExamName == null ? exam.Name : examDTO.ExamName;
            if (examDTO.Teacher != null)
            {
                exam.userTeacher = (from i in db.userTeachers where i.Id == (int)examDTO.Teacher select i).SingleOrDefault();
            }
            db.Update(exam);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> DeleteExam(int Id, ExamContext db)
        {
            var exam = (from i in db.exams
                        where i.Id == Id
                        select i).SingleOrDefault();
            if (exam == null)
            {
                return Results.NotFound("Object Not Existed");
            }
            db.Remove(exam);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        #endregion
        ///试卷题目
        #region
        public async Task<IResult> AddQuestionExam(QuestionExamDTO questionExamDTO, ExamContext db)
        {
            var questionExam = new Question();
            questionExam.ExamId = questionExamDTO.Exam;
            var exam = (from i in db.exams
                        where i.Id == questionExamDTO.Exam
                        select i).SingleOrDefault();
            if (exam == null)
                return Results.NotFound("Exam Not Existed");
            questionExam.exam = exam;
            var question = (from i in db.QuestionContext
                            where i.Id == questionExamDTO.Question
                            select i).SingleOrDefault();
            if (question == null)
                return Results.NotFound("Question Not Existed");
            questionExam.QuestionContextId = questionExamDTO.Question;
            questionExam.questionContext = question;
            db.Add(questionExam);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> SelectQuestionExam(string exam, ExamContext db)
        {
            var result = new List<QuestionExamDTO>();
            var questions = from i in db.questions
                            where i.ExamId == Convert.ToInt32(exam)
                            select i;
            foreach (var i in questions)
                result.Add(new QuestionExamDTO { Exam = i.ExamId, Question = i.QuestionContextId });
            return Results.Ok(result);
        }

        public async Task<IResult> SelectQuestionExamScore(string exam, ExamContext db)
        {
            var result = new List<QuestionExamDTO>();
            var rtquestions = db.questions.Where( i => i.questionContext.questionChoice == null && i.ExamId == int.Parse(exam)).Select( i => i.questionContext.Cost).ToList();
            int tscore = rtquestions.Sum();
            int tl = rtquestions.Count();
            
            var rcquestions = db.questions.Where(i => i.questionContext.questionChoice != null && i.ExamId == int.Parse(exam)).Select(i => i.questionContext.Cost).ToList();
            int cscore = rcquestions.Sum();
            int cl = rcquestions.Count();

            var Tscore = new score { cost = tscore, count = tl };
            var Cscore = new score { cost = cscore, count = cl };
            
            return Results.Ok(new { Cscore,Tscore });
        }
        class score
        {
           public int cost { get; set; }
           public  int count { get; set; }
        }

        public async Task<IResult> UpdateQuestionExam(QuestionExamDTO questionExamDTO, QuestionExamDTO questionExamDTO2, ExamContext db)
        {
            var question = (from i in db.questions
                            where i.ExamId == questionExamDTO.Exam && i.QuestionContextId == questionExamDTO.Question
                            select i).SingleOrDefault();
            if (question == null)
                return Results.NotFound("Object Not Existed");
            question.ExamId = questionExamDTO2.Exam;
            question.QuestionContextId = questionExamDTO2.Question;
            var exam = (from i in db.exams
                        where i.Id == questionExamDTO.Exam
                        select i).SingleOrDefault();
            if (exam == null)
                return Results.NotFound("Exam Not Existed");
            question.exam = exam;
            var question2 = (from i in db.QuestionContext
                             where i.Id == questionExamDTO2.Question
                             select i).SingleOrDefault();
            if (question2 == null)
                return Results.NotFound("Question Not Existed");
            question.questionContext = question2;
            db.Update(question);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> DeleteQuestionExam(QuestionExamDTO questionExamDTO, ExamContext db)
        {
            var question = from i in db.questions
                            where i.ExamId == questionExamDTO.Exam && i.QuestionContextId == questionExamDTO.Question
                            select i;
            if (question.Count() == 0)
                return Results.NotFound("Object Not Existed");
            db.RemoveRange(question);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        #endregion
        ///学生答卷
        #region
        public async Task<IResult> AddAnswer(StudenAnswerDTO studenAnswerDTO, ExamContext db)
        {
            var studentAnswer = new StudentQuestionAnswer
            {
                QuestionId = (int)studenAnswerDTO.QuestionId,
                UserStudentId = (int)studenAnswerDTO.StudentId,
                ChoiceAnswer = studenAnswerDTO.ChoiceAnswer,
                TxtAnswer = studenAnswerDTO.TxtAnswer,
            };
            var question = (from i in db.questions
                            where i.Id == studenAnswerDTO.QuestionId
                            select i).SingleOrDefault();
            var questionCount = (from i in db.StudentQuestionAnswers
                                 where i.QuestionId == studenAnswerDTO.QuestionId
                                 select i).Count();
            var userStudent = (from i in db.userStudents
                               where i.Id == studenAnswerDTO.StudentId
                               select i).SingleOrDefault();
            if (question == null)
                return Results.BadRequest();
            if (userStudent == null)
                return Results.BadRequest();
            studentAnswer.question = question;
            studentAnswer.userStudent = userStudent;
            if(questionCount <= 0)
                db.Add(studentAnswer);
            else
            {
                var lquestion = (from i in db.StudentQuestionAnswers
                                where i.QuestionId == studenAnswerDTO.QuestionId && i.UserStudentId == studenAnswerDTO.StudentId
                                select i).FirstOrDefault();
                var qut = (from i in db.StudentQuestionAnswers
                           join j in db.questions on i.QuestionId equals j.Id
                           where j.Id == lquestion.QuestionId
                           select j.questionContext.QuestionChoiceId != null).ToList();
                if (qut.Count > 0)
                    lquestion.ChoiceAnswer = studenAnswerDTO.ChoiceAnswer;
                else
                    lquestion.TxtAnswer = studenAnswerDTO.TxtAnswer;
                db.Update(lquestion);
            }
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> UpdateAnswer(string studentId, string question, string exam, StudenAnswerDTO studenAnswerDTO, ExamContext db)
        {

            var answer = (from i in db.StudentQuestionAnswers
                          where Convert.ToInt32(studentId) == i.UserStudentId && Convert.ToInt32(question) == i.QuestionId && i.question.ExamId == Convert.ToInt32(exam)
                          select i).SingleOrDefault();
            answer.ChoiceAnswer = studenAnswerDTO.ChoiceAnswer;
            answer.TxtAnswer = studenAnswerDTO.TxtAnswer;
            db.Update(answer);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> GetAnswer(string studentId, string exam, ExamContext db)
        {
            var result = new List<StudenAnswerDTO>();
            var answer = from i in db.StudentQuestionAnswers
                         where i.question.ExamId == Convert.ToInt32(exam) && i.UserStudentId == Convert.ToInt32(studentId)
                         select i;
            if (answer.FirstOrDefault() == null)
                return Results.BadRequest("Object Not Existed");
            foreach (var i in answer)
                result.Add(new StudenAnswerDTO
                {
                    Id = i.Id,
                    StudentId = Convert.ToInt32(i.UserStudentId),
                    QuestionId = i.QuestionId,
                    ChoiceAnswer = i.ChoiceAnswer,
                    TxtAnswer = i.TxtAnswer
                });
            return Results.Ok(result);
        }
        public async Task<IResult> DeleteAnswer(string studentId, ExamContext db)
        {
            var answer = (from i in db.StudentQuestionAnswers
                          where i.UserStudentId == Convert.ToInt32(studentId)
                          select i);
            db.RemoveRange(answer);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        #endregion
        //学生分数
        #region
        public async Task<IResult> getScore(string stu, string exam, ExamContext db)
        {
            var result = (from i in db.studentExams
                          where i.UserStudentId == Convert.ToInt32(stu) && i.ExamId == Convert.ToInt32(exam)
                          select i).SingleOrDefault();
            return Results.Ok(result);
        }
        public async Task<IResult> updateScore(string stu, string exam, string score, ExamContext db)
        {
            var newscore = (from i in db.studentExams
                            where i.UserStudentId == Convert.ToInt32(stu) && i.ExamId == Convert.ToInt32(exam)
                            select i.score).SingleOrDefault();
            newscore.score = Convert.ToInt32(score);
            db.Update(newscore);
            await db.SaveChangesAsync();
            return Results.Ok();
        }
        public async Task<IResult> deleteScore(string stu, string exam, ExamContext db)
        {
            var result = (from i in db.studentExams
                          where i.UserStudentId == Convert.ToInt32(stu) && i.ExamId == Convert.ToInt32(exam)
                          select i).Include(x => x.score).SingleOrDefault();
            db.Remove(result);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        public async Task<IResult> insertScore(StuExamDTO stuExamDTO, ExamContext db)
        {
            var stuExam = new StudentExam
            {
                UserStudentId = (int)stuExamDTO.UserStudentId,
                ExamId = (int)stuExamDTO.ExamId,
                userStudent = (from i in db.userStudents where i.Id == (int)stuExamDTO.UserStudentId select i).SingleOrDefault(),
                exam = (from i in db.exams where i.Id == (int)stuExamDTO.ExamId select i).SingleOrDefault(),
                score = new Score
                {
                    score = 0,
                }
            };
            db.Add(stuExam);
            await db.SaveChangesAsync();
            return Results.Ok("success");
        }
        #endregion
    }
    //DTO
    #region 
    //用户与班级DTO
    #region
    /// <summary>
    /// 用于获取与反馈用户信息的DTO
    /// </summary>
    public class UserDTO
    {
        public UserStudent? userStudent { get; set; }
        public UserTeacher? userTeacher { get; set; }
        public string? role { get; set; }
    }

    /// <summary>
    /// 用于获取与反馈用户班级信息的DTO
    /// </summary>
    public class UserClassDTO
    {
        public string? Account { get; set; }
        public string? ClassName { get; set; }
    }
    #endregion
    //题库的DTO
    #region
    public class QuestionDTO
    {
        public int? Id { get; set; }
        public string QuestionClass { get; set; }
        public string Stem { get; set; }
        public int Cost { get; set; }
        public int DifficutlyLevels { get; set; }
        public string Subject { get; set; }
        public ChoiceOption? ChoiceOptions { get; set; }
        public int? ChoiceAnswer { get; set; }
        public string? TxtAnswer { get; set; }
    }
    public class ChoiceOption
    {
        public string? ChoiceA { get; set; }
        public string? ChoiceB { get; set; }
        public string? ChoiceC { get; set; }
        public string? ChoiceD { get; set; }
    }

    public class ResultQuestionDTO
    {
        public List<QuestionDTO> questionDTOs { get; set; }
    }
    #endregion
    //试卷的DTO
    #region
    public class ExamDTO
    {
        public int? Id { get; set; }
        public string? ExamName { get; set; }
        public string? ExamDate { get; set; }
        public float? TimeLong { get; set; }
        public int? Teacher { get; set; }
        public int? ExamScore { get; set; }

    }
    public class ResultExamDTO
    {
        public List<ExamDTO> exams { get; set; }
    }
    #endregion
    //试卷题目DTO
    #region
    public class QuestionExamDTO
    {
        public int Question { get; set; }
        public int Exam { get; set; }
    }
    #endregion
    //学生答案DTO
    #region
    public class StudenAnswerDTO
    {
        public int? Id { get; set; }
        public int? QuestionId { get; set; }
        public int? StudentId { get; set; }
        public int? ChoiceAnswer { get; set; }
        public string? TxtAnswer { get; set; }
    }
    public class StuExamDTO
    {
        public int? StuExamId { get; set; }
        public int? ExamId { get; set; }
        public int? UserStudentId { get; set; }
    }
    #endregion
    #endregion
}
