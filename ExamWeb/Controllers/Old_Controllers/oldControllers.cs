using Microsoft.AspNetCore.Mvc;
using ExamWebEF;
using OldExam;
using ExamWeb.DTO;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using ExamWebEF;
using System.Linq;

namespace ExamWeb.Controllers.Old_Controllers
{
    public class oldControllers : ControllerBase
    {
        [HttpPost("/users/adduser")]
        public async Task<IResult> addUser([FromBody] UserDTO userDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().DBInsertUser(userDTO, db);
        }
        //[HttpGet("/users/stu")]
        //public async Task<IResult> getStu([FromServices] ExamContext db)
        //{
        //    return await new DBOp().DBFindUserInfo(account: "%STU%", db);
        //}

        [HttpGet("/users/teacher")]
        public async Task<IResult> getTeacher([FromServices] ExamContext db)
        {
            return await new DBOp().DBFindUserInfo(account: "%TEACHER%", db);
        }
        [HttpGet("/users/{account}")]
        public  ActionResult<UserStudent> getAccount(string account, [FromServices] ExamContext db)
        {
            return  new DBOp().SDBFindUserInfo(account, db);
        }
        [HttpGet("/users/{account}&{password}")]
        public async Task<IResult> getAccountPwd(string account, string password, [FromServices] ExamContext db)
        {
            return await new DBOp().DBLogin(account, password, db);
        }
        
        [HttpPut("/users/{account}")]
        public async Task<IResult> PutAcc(string account,[FromBody] UserDTO userDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().DBUpdateUserInfo(account, userDTO, db);
        }
        [HttpDelete("/users/{account}")]
        public async Task<IResult> DelAcc(string account, [FromServices] ExamContext db)
        {
        return await new DBOp().DBDeleteUserInfo(account, db);
        }

        [HttpPost("/class/UserClass/{account}")]
        public async Task<IResult> PostClass(string account,[FromBody] UserClassDTO userClassDTO, [FromServices] ExamContext db) 
        {
            return await new DBOp().DBUserAddUserClass(account, userClassDTO, db);

        }
        [HttpGet("/class/UserClass/{account}")]
        public async Task<IResult> GetClass(string account, [FromServices] ExamContext db) 
        {
            return await new DBOp().DBUserGetUserClass(account, db);

        }
        [HttpPut("/class/UserClass/{account}")]
        public async Task<IResult> PutClass(string account, UserClassDTO userClassDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().DBUserPutUserClass(account, userClassDTO, db);
        }
        [HttpDelete("/class/UserClass/{account}&{className}")]
        public async Task<IResult> DelClass(string account, string className, [FromServices] ExamContext db) 
        {
            return await new DBOp().DBDeleteUserClass(account, className, db);
        }

        [HttpPost("/class/addClass")]
        public async Task<IResult> PostOneClass(UserClassDTO userClassDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().DBPostClass(userClassDTO, db);

        }
        [HttpPut("/class/{className}")]
        public async Task<IResult> PutOneClass(string className, UserClassDTO userClassDTO, [FromServices] ExamContext db) 
        {
            return await new DBOp().DBPutClass(className, userClassDTO, db);
        }
        [HttpGet("/class/{className}")]
        public async Task<IResult> GetOneClass(string className, [FromServices] ExamContext db)
        {
            return await new DBOp().DBGetClass(className, db);
        }
        [HttpGet("/class")]
        public async Task<IResult> GetMClass([FromServices] ExamContext db)
        {
            return await new DBOp().DBGetClass(className: null, db);
        }
        [HttpDelete("/class/{className}")]
        public async Task<IResult> DelOneClass(string className, [FromServices] ExamContext db)
        {
            return await new DBOp().DBDeleteClass(className, db);
        }

        [HttpPost("/tquestion/add")]
        public async Task<IResult> Postt([FromBody] Tquestion tquestion, [FromServices] ExamContext db)
        {
            QuestionDTO questionDTO = new QuestionDTO();
            questionDTO.Subject = tquestion.subject;
            questionDTO.TxtAnswer = tquestion.answer;
            questionDTO.QuestionClass = tquestion.istype;
            questionDTO.DifficutlyLevels = int.Parse(tquestion.level);
            questionDTO.Cost = int.Parse(tquestion.cost);
            questionDTO.Stem = tquestion.question;
            return await new DBOp().InsertQuestion(questionDTO, db);
        }
        [HttpPost("/cquestion/add")]
        public async Task<IResult> Postc([FromBody] Cquestion cquestion, [FromServices] ExamContext db)
        {
            Dictionary<string,int> choice = new Dictionary<string,int>();
            choice.Add("A", 1);
            choice.Add("B", 2);
            choice.Add("C", 3);
            choice.Add("D", 4);
            QuestionDTO questionDTO = new QuestionDTO();
            questionDTO.Subject = cquestion.subject;
            questionDTO.Cost = int.Parse(cquestion.cost);
            questionDTO.DifficutlyLevels=int.Parse(cquestion.level);
            questionDTO.QuestionClass=cquestion.istype;
            questionDTO.ChoiceAnswer = choice[cquestion.rightAnswer];
            ChoiceOption questionChoice = new ChoiceOption();
            questionChoice.ChoiceA = cquestion.answerA;
            questionChoice.ChoiceB = cquestion.answerB;
            questionChoice.ChoiceC = cquestion.answerC;
            questionChoice.ChoiceD = cquestion.answerD;
            questionDTO.ChoiceOptions = questionChoice;
            questionDTO.Stem = cquestion.question;
            questionDTO.QuestionClass = cquestion.istype;
            return await new DBOp().InsertQuestion(questionDTO, db);
        }

        [HttpGet("/question/search/Id={Id}&QuestionClass={QuestionClass}&Stem={Stem}&Cost={Cost}&DifficutlyLevels={DifficutlyLevels}&Subject={Subject}&ChoiceAnswer={ChoiceAnswer}&TxtAnswer={TxtAnswer}&ChoiceA={ChoiceA}&ChoiceB={ChoiceB}&ChoiceC={ChoiceC}&ChoiceD={ChoiceD}")]
        public async Task<IResult> GetSomeQuestion(string Id, string QuestionClass, string Stem, string Cost, string DifficutlyLevels, string Subject, string ChoiceAnswer, string ChoiceA, string ChoiceB, string ChoiceC, string ChoiceD, string TxtAnswer, [FromServices] ExamContext db)
        {
            if (Id == "null")
                Id = null;
            if (QuestionClass == "null")
                QuestionClass = null;
            if (Stem == "null")
                Stem = null;
            if (Cost == "null")
                Cost = null;
            if (DifficutlyLevels == "null")
                DifficutlyLevels = null;
            if (Subject == "null")
                Subject = null;
            if (ChoiceAnswer == "null")
                ChoiceAnswer = null;
            if (ChoiceA == "null")
                ChoiceA = null;
            if (TxtAnswer == "null")
                TxtAnswer = null;
            if (ChoiceB == "null")
                ChoiceB = null;
            if (ChoiceC == "null")
                ChoiceC = null;
            if (ChoiceD == "null")
                ChoiceD = null;
            if (Subject == "null")
                Subject = null;

            QuestionDTO questionDTO = new QuestionDTO
            {
                Id = Convert.ToInt32(Id),
                QuestionClass = QuestionClass,
                Stem = Stem,
                Cost = Convert.ToInt32(Cost),
                DifficutlyLevels = Convert.ToInt32(DifficutlyLevels),
                Subject = Subject,
                ChoiceAnswer = Convert.ToInt32(ChoiceAnswer),
                TxtAnswer = TxtAnswer,
                ChoiceOptions = new ChoiceOption
                {
                    ChoiceA = ChoiceA,
                    ChoiceB = ChoiceB,
                    ChoiceC = ChoiceC,
                    ChoiceD = ChoiceD,
                }
            };
            return await new DBOp().SeletQuestion(questionDTO, db);

        }

        [HttpPut("/question/update/Id ={Id}")]
        public async Task<IResult> PutQuestion(string Id, QuestionDTO questionDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().UpdateQuestion(Convert.ToInt32(Id), questionDTO, db);
        }

        [HttpDelete("/question/delete/Id={Id}")]
        public async Task<IResult> DelQuestion(string Id, [FromServices] ExamContext db)
        {
            return await new DBOp().DeleteQuestion(Convert.ToInt32(Id), db);
        }

        [HttpPost("/exam/add/grade={grade}")]
        public async Task<IResult> Postexam(string grade,[FromBody] ExamDTO exam, [FromServices] ExamContext db)
        {
            return await new DBOp().AddExam(grade,exam, db);
        }

        [HttpGet("/exam/exams/{userId}")]
        public List<Exam> GetExams(string userId,[FromServices] ExamContext db)
        {
            var allexams = db.exams.Where(i => i.studentExams.Count > 0 && i.Date > DateTime.Now || i.Date < DateTime.Now.AddMinutes(5.0)).Where(i => i.questions.Count > 0)
                    .Include(i => i.studentExams).Include(j => j.questions).ToList();
            var rexams = new List<Exam>();
            var examed = (from i in allexams
                          join j in db.questions on i.Id equals j.ExamId
                          join k in db.StudentQuestionAnswers on j.Id equals k.QuestionId
                          select i.Id).ToList();
            foreach (var i in allexams)
            {
                if(i.studentExams != null && !examed.Contains(i.Id))
                {
                    foreach(var j in i.studentExams)
                    {
                        if (userId.Contains('&'))
                            userId = userId.Remove(userId.LastIndexOf('&'));
                        if(j.UserStudentId == int.Parse(userId))
                            rexams.Add(i);
                    }
                }
            }
            return rexams;
        }

        [HttpGet("/exam/exams/{userId}&{key}")]
        public List<Exam> GetKeyExams(string userId,string key, [FromServices] ExamContext db)
        {
            var allexams = db.exams.Where(i => i.studentExams.Count > 0
                        && i.Name.Contains(key)
                        && i.Date > DateTime.Now
                        || i.Date < DateTime.Now.AddMinutes(5.0)
                        ).Where(i => i.questions.Count > 0)
                    .Include(i => i.studentExams).Include(j => j.questions).ToList();

            var rexams = new List<Exam>();
            var examed = (from i in allexams
                        join j in db.questions on i.Id equals j.ExamId
                        join k in db.StudentQuestionAnswers on j.Id equals k.QuestionId
                        select i.Id).ToList();
            foreach (var i in allexams)
            {
                if (i.studentExams != null && !examed.Contains(i.Id))
                {
                    foreach (var j in i.studentExams)
                    {
                        if (j.UserStudentId == int.Parse(userId))
                            rexams.Add(i);
                    }
                }
            }
            return rexams;
        }

        [HttpGet("/questions/{eid}")]
        public IResult Getquestions(string eid,[FromServices] ExamContext db)
        {
            var cquestions = from i in db.questions
                             join j in db.QuestionContext on i.QuestionContextId equals j.Id
                             join k in db.QuestionChoice on j.QuestionChoiceId equals k.Id
                             where i.ExamId == int.Parse(eid) && j.questionChoice != null
                             select new {j.Id,qclass ="choice", j.Stem, k.ChoiceA, k.ChoiceB, k.ChoiceC, k.ChoiceD};
            var tquestions = from i in db.questions
                             join j in db.QuestionContext on i.QuestionContextId equals j.Id
                             where j.questionChoice == null && i.ExamId == int.Parse(eid)
                             select new { j.Id, qclass = "txt", j.Stem };
            return Results.Ok(new { cquestions, tquestions });

        }

        [HttpGet("/exam/Id={Id}&ExamName={ExamName}&ExamData={ExamDate}&TimeLong={TimeLong}&Teacher={Teacher}&ExamScore={ExamScore}")]
        public async Task<IResult> Getexam(string Id, string ExamName, string ExamDate, string TimeLong, string Teacher, string ExamScore, [FromServices] ExamContext db)
        {
            if (Id == "null")
                Id = null;
            if (ExamName == "null")
                ExamName = null;
            if (ExamDate == "null")
                ExamDate = null;
            if (TimeLong == "null")
                TimeLong = null;
            if (Teacher == "null")
                Teacher = null;
            if (ExamScore == "null")
                ExamScore = null;
            var examDTO = new ExamDTO
            {
                Id = Convert.ToInt32(Id),
                ExamName = ExamName,
                ExamDate = ExamDate,
                TimeLong = Convert.ToInt32(TimeLong),
                Teacher = Convert.ToInt32(Teacher),
                ExamScore = Convert.ToInt32(ExamScore)
            };
            return await new DBOp().GetExam(examDTO, db);

        }
        [HttpDelete("/exam/Id={Id}")]
        public async Task<IResult> Delexam(string Id, [FromServices] ExamContext db)
        {
            int id = Convert.ToInt32(Id);
            return await new DBOp().DeleteExam(id, db);
        }
        [HttpPut("/exam/Id={Id}")]
        public async Task<IResult> Putexam(string Id,[FromBody] ExamDTO exam, [FromServices] ExamContext db)
        {
            exam.Id = int.Parse(Id);
            return await new DBOp().UpdateExam(exam, db);

        }

        [HttpPost("/questionExam/add")]
        public async Task<IResult> PostQuestionExam([FromBody] QuestionExamDTO questionExamDTO, [FromServices] ExamContext db) 
        {
            return await new DBOp().AddQuestionExam(questionExamDTO, db);
        }
        [HttpGet("/questionExam/Exam={exam}")]
        public async Task<IResult> GetQuestionExam(string exam, [FromServices] ExamContext db)
        {
            return await new DBOp().SelectQuestionExam(exam, db);
        }

        [HttpGet("/questionExamScore/Exam={exam}")]
        public async Task<IResult> GetQuestionExamScore(string exam, [FromServices] ExamContext db)
        {
            return await new DBOp().SelectQuestionExamScore(exam, db);
        }


        [HttpPut("/quesiontExam/Exam={exam}&Question={question}")]
        public async Task<IResult> PutQuestionExam(string exam, string question, QuestionExamDTO questionExamDTO2, [FromServices] ExamContext db)
        {
            QuestionExamDTO questionExamDTO = new QuestionExamDTO();
            questionExamDTO.Exam = Convert.ToInt32(exam);
            questionExamDTO.Question = Convert.ToInt32(question);
            return await new DBOp().UpdateQuestionExam(questionExamDTO, questionExamDTO2, db);

        }
        [HttpDelete("/quesitionExam/Exam={exam}&Question={question}")]
        public async Task<IResult> DelQuestionExam(string exam,string question, [FromServices] ExamContext db)
        {
            QuestionExamDTO questionExamDTO = new QuestionExamDTO();
            questionExamDTO.Exam = Convert.ToInt32(exam);
            questionExamDTO.Question = Convert.ToInt32(question);
            return await new DBOp().DeleteQuestionExam(questionExamDTO, db);

        }

        [HttpPost("/studentAnswer/add")]
        public async Task<IResult> PostStudentAnswer([FromBody] StudenAnswerDTO studenAnswerDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().AddAnswer(studenAnswerDTO, db);
        }
        [HttpGet("/studentAnswer/studentId={studentId}&Exam={exam}")]
        public async Task<IResult> GetStudentAnswer(string studentId, string exam, [FromServices] ExamContext db)
        {
            return await new DBOp().GetAnswer(studentId, exam, db);
        }
        [HttpPut("/studentAnswer/studentId={studentId}&Question={question}&Exam={exam}")]
        public async Task<IResult> PutStudentAnswer(string studentId, string question, string exam, StudenAnswerDTO studenAnswerDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().UpdateAnswer(studentId, question, exam, studenAnswerDTO, db);
        }
        [HttpDelete("/studentAnswer/Id={Id}")]
        public async Task<IResult> DelStudentAnswer(string Id, [FromServices] ExamContext db)
        {
            return await new DBOp().DeleteAnswer(Id, db);
        }
        [HttpGet("/questionExamAnswer/questionId={quId}&stuId={stuId}")]
        public string GetQuestionStuAnswer(string quId,string stuId,[FromServices] ExamContext db)
        {
            string ranswer;
            var qanswer = (from i in db.StudentQuestionAnswers
                           where i.QuestionId == Convert.ToInt32(quId) && i.UserStudentId == Convert.ToInt32(stuId)
                           select i).FirstOrDefault();
            if(qanswer == null)
                return "";
            var choice = (from i in db.QuestionContext
                         join j in db.questions on i.Id equals j.QuestionContextId
                         where j.Id == qanswer.QuestionId
                         select i.QuestionChoiceId).FirstOrDefault();
            if (choice != null)
                ranswer = qanswer.ChoiceAnswer.ToString();
            else
                ranswer = qanswer.TxtAnswer;
            return ranswer;
        }

        

        [HttpPost("/stuExam/add")]
        public async Task<IResult> PostStuExam(StuExamDTO stuExamDTO, [FromServices] ExamContext db)
        {
            return await new DBOp().insertScore(stuExamDTO, db);
        }
        [HttpPut("/stuExam/stu={stu}&exam={exam}&score={score}")]
        public async Task<IResult> PutStuExam(string stu, string exam, string score, [FromServices] ExamContext db)
        {
            return await new DBOp().updateScore(stu, exam, score, db);
        }
        [HttpGet("/stuExam/stu={stu}&exam={exam}")]
        public async Task<IResult> GetStuExam(string stu, string exam, [FromServices] ExamContext db)
        {
            return await new DBOp().getScore(stu, exam, db);
        }
        [HttpDelete("/stuExam/stu={stu}&exam={exam}")]
        public async Task<IResult> DelStuExam(string stu, string exam, [FromServices] ExamContext db)
        {
            return await new DBOp().deleteScore(stu, exam, db);
        }

        [HttpGet("/questionId/Exam={exam}&Question={question}")]
        public int getQuestionId(string exam,string question,[FromServices] ExamContext db)
        {
            var rquestionid = from i in db.questions
                             where i.QuestionContextId == int.Parse(question) && i.ExamId == int.Parse(exam)
                             select i.Id;
            return rquestionid.FirstOrDefault();
        }
        [HttpGet("/questionAnswerInfo/{key?}")]
        public List<ExamAnswerInfo> getExamAnswerInfo(string? key, [FromServices] ExamContext db)
        {
            string ikey = key ?? string.Empty;
            var exam = (from i in db.StudentQuestionAnswers
                       where (i.userStudent.Name.Contains(ikey) || i.question.exam.Name.Contains(ikey) ) && (i.question.questionContext.questionChoice == null)
                       select new
                       {
                           stuId = i.UserStudentId,
                           examId = i.question.ExamId,
                           examName = i.question.exam.Name,
                           examClass = i.userStudent.userStudentClass.classes.Name,
                           examStu = i.userStudent.Name,
                           questionId = i.QuestionId,
                           stuacc = i.userStudent.Account,
                       }).ToList();
            List<ExamAnswerInfo> rexamAnswerInfo = new List<ExamAnswerInfo>();
            foreach(var e in exam)
            {
                var i = new ExamAnswerInfo
                {
                    stuId = e.stuId,
                    examId = e.examId,
                    examName = e.examName,
                    examClass = e.examClass,
                    examStu = e.examStu,
                    questionId = e.questionId.ToString(),
                    stuacc = e.stuacc
                };
                rexamAnswerInfo.Add(i);
            }
            var r = rexamAnswerInfo.Distinct( new Comparer() ).ToList();
            return r;
        }

        [HttpGet("/questionanswer/{stuid}&{questionid}")]
        public questionAnswerModel getquestionAnswer(string stuid,string questionid,[FromServices] ExamContext db)
        {
            var t = (from i in db.questions
                    where int.Parse(questionid) == i.Id
                    select i.questionContext.Stem).FirstOrDefault();
            var a = (from i in db.StudentQuestionAnswers
                    where i.QuestionId == int.Parse(questionid) && i.UserStudentId == int.Parse(stuid)
                    select i.TxtAnswer).FirstOrDefault();
            var r = new questionAnswerModel
            {
                question = t,
                studentAnswer = a
            };
            return r;
        }

        [HttpGet("/studentscore/{score}&{examid}&{studentid}&{questionId}")]
        public IResult addScore(string score,string examid, string studentid,string questionId, [FromServices] ExamContext db)
        {
            var t  =  (from i in db.studentExams
                      where int.Parse(examid) == i.ExamId && int.Parse(studentid) == i.UserStudentId
                      select i.Id).SingleOrDefault();
            var s = (from i in db.Score
                    where i.StudentExamId == t
                    select i).SingleOrDefault();
            Score score1 = new Score {
                StudentExamId = t,
                score = int.Parse(score),
            };
            if (s == null)
                db.Add(score1);
            else
            {
                s.score += int.Parse(score);
                db.Update(s);
            }
            var stuanswer = (from i in db.StudentQuestionAnswers
                             where i.QuestionId == int.Parse(questionId) && int.Parse(studentid) == i.UserStudentId
                             select i).FirstOrDefault();
            db.Remove(stuanswer!);
            db.SaveChanges();
            return Results.Ok();
        }

        [HttpGet("/allexamscore")]
        public List<examScore> getallexamScore([FromServices] ExamContext db)
        {
            var t = (from i in db.Score
                    select new
                    {
                        score = i.score,
                        seid = i.StudentExamId
                    }).ToList();
            
            var r = new List<examScore>();
            foreach(var i in t)
            {
                var stuEx = (from j in db.studentExams
                            where j.Id == i.seid
                            select j).Include(k => k.userStudent).ThenInclude(m => m.userStudentClass).ThenInclude(n => n.classes).Include(o => o.exam).FirstOrDefault();
                var m = new examScore
                {
                    stuName = stuEx.userStudent.Name,
                    stuClass = stuEx.userStudent.userStudentClass.classes.Name,
                    stuExam = stuEx.exam.Name,
                    stuScore = i.score
                };
                r.Add(m);
            }
            return r;
        }

        [HttpGet("/submitExam/{stuid}/{examid}")]
        public bool SumStudentScore(string stuid,string examid,[FromServices] ExamContext db)
        {
            int score = 0;
            var questions = (from i in db.questions
                            where i.ExamId == int.Parse(examid)
                            select i).Include(x => x.exam).ToList();
            var answers = (from j in questions 
                          join i in db.StudentQuestionAnswers on j.Id equals i.QuestionId
                          where i.UserStudentId == int.Parse(stuid)
                          select i).ToList();
            var questionContext = (from j in answers
                             join i in db.QuestionContext on j.question.QuestionContextId equals i.Id
                                   select i).ToList();
            foreach(var i in questionContext)
            {
                var answer = answers.Where(j => j.QuestionId == i.question.Id).FirstOrDefault();
                var choiceanswer = db.Answer.Where(x => x.Id == i.Id).Select(x => x.ChoiceAnswer).FirstOrDefault();
                if (answer != null && choiceanswer != null && answer.ChoiceAnswer == choiceanswer)
                    score += i.Cost;
            }
            var Score = new Score
            {
                score = score,
                StudentExamId = (from i in db.studentExams where i.UserStudentId == int.Parse(stuid) && i.ExamId == int.Parse(examid) select i.Id).FirstOrDefault(),
            };
            db.Add(Score);
            db.SaveChanges();
            return true;
        }

        [HttpGet("/studentscore/{stuid}")]
        public List<examScore> getstudentScore(string stuid, [FromServices] ExamContext db)
        {
            var t = (from i in db.Score
                     select new
                     {
                         score = i.score,
                         seid = i.StudentExamId
                     }).ToList();

            var r = new List<examScore>();
            foreach (var i in t)
            {
                var stuEx = (from j in db.studentExams
                             where j.Id == i.seid && j.UserStudentId == int.Parse(stuid)
                             select j).Include(k => k.userStudent).ThenInclude(m => m.userStudentClass).ThenInclude(n => n.classes).Include(o => o.exam).FirstOrDefault();
                var m = new examScore
                {
                    stuName = stuEx.userStudent.Name,
                    stuClass = stuEx.userStudent.userStudentClass.classes.Name,
                    stuExam = stuEx.exam.Name,
                    stuScore = i.score
                };
                r.Add(m);
            }
            return r;
        }

    }
    class Comparer : IEqualityComparer<ExamAnswerInfo>
    {
        public bool Equals(ExamAnswerInfo? x, ExamAnswerInfo? y)
        {
            return x.stuId == y.stuId && x.examId == y.examId && x.examName == y.examName && x.examClass == y.examClass && x.examStu == y.examStu;
        }
        public int GetHashCode( ExamAnswerInfo obj)
        {
            return obj.GetHashCode();
        }
    }
}

