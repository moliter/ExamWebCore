using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ExamWebEF
{
    /// <summary>
    /// 学生表
    /// </summary>
    public class UserStudent
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Account { get; set; }

        [Required, MaxLength(50), MinLength(6)]
        public string Password { get; set; }

        [Required, MaxLength(50), MinLength(1)]
        public string Name { get; set; }
        public UserStudentClass userStudentClass { get; set; }
        public List<StudentExam> studentExams { get; set; }


    }

    /// <summary>
    /// 教师表
    /// </summary>
    public class UserTeacher
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Account { get; set; }

        [Required, MaxLength(50), MinLength(6)]
        public string Password { get; set; }

        [Required, MaxLength(50), MinLength(1)]
        public string Name { get; set; }
        public List<UserTeacherClass> userTeacherClass { get; set; }
        public List<Exam> exams { get; set; }
    }

    /// <summary>
    /// 班级表
    /// </summary>
    public class UserStudentClass
    {
        public int Id { get; set; }
        public int UserStudentId { get; set; }
        public UserStudent userStudent { get; set; }
        public int ClassId { get; set; }
        public Class classes { get; set; }
    }

    public class UserTeacherClass
    {
        public int Id { get; set; }
        public int UserTeacherId { get; set; }
        public UserTeacher UserTeacher { get; set; }
        public int ClassId { get; set; }
        public Class classes { get; set; }
    }


    /// <summary>
    /// 班级表
    /// </summary>
    public class Class
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
    }

    public class Exam
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required, Column(TypeName = "datetime")]
        public DateTime Date { get; set; }
        [Required]
        public float Time { get; set; }
        [Required]
        public int UserTeacherId { get; set; }
        public int Score { get; set; }
        public UserTeacher userTeacher { get; set; }
        public List<Question> questions { get; set; }
        public List<StudentExam> studentExams { get; set; }
    }

    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int QuestionContextId { get; set; }
        public QuestionContext questionContext { get; set; }
        [Required]
        public int ExamId { get; set; }
        public Exam exam { get; set; }
        public string cost { get; set; }
    }

    public class StudentExam
    {
        [Required]
        public int Id { get; set; }
        public int ExamId { get; set; }
        public Exam exam { get; set; }
        public int UserStudentId { get; set; }
        public UserStudent userStudent { get; set; }
        public Score score { get; set; }

    }

    public class Score
    {
        public int Id { get; set; }
        public int StudentExamId { get; set; }
        public StudentExam studentExam { get; set; }
        public int score { get; set; }
    }

    public class QuestionContext
    {
        public int Id { get; set; }
        public string Stem { get; set; }
        public int? QuestionChoiceId { get; set; }
        public QuestionChoice? questionChoice { get; set; }
        public int AnswerId { get; set; }
        public Answer answer { get; set; }
        public int DifficultyLevel { get; set; }
        public string Subject { get; set; }
        public int Cost { get; set; }
        public Question question { get; set; }

    }

    public class Answer
    {
        public int Id { get; set; }
        public int? ChoiceAnswer { get; set; }
        public string? TxtAnswer { get; set; }
        public QuestionContext questionContext { get; set; }
    }

    public class QuestionChoice
    {
        public int Id { get; set; }
        public string? ChoiceA { get; set; }
        public string? ChoiceB { get; set; }
        public string? ChoiceC { get; set; }
        public string? ChoiceD { get; set; }
        public QuestionContext questionContext { get; set; }
    }

    public class StudentQuestionAnswer
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public Question question { get; set; }
        public int UserStudentId { get; set; }
        public UserStudent userStudent { get; set; }
        public int? ChoiceAnswer { get; set; }
        public String? TxtAnswer { get; set; }
    }
    public class ExamAnswerInfo
    {
        public int stuId { get; set; }
        public int examId { get; set; }

        public string examName { get; set; }

        public string examClass { get; set; }
        public string examStu { get; set; }

        public string questionId { get; set; }
        public string stuacc { get; set; }

    }

    public class questionAnswerModel
    {
        public string question { get; set; }
        public string studentAnswer { get; set; }
    }

    public class examScore
    {
        public string stuName { get; set; }
        public string stuClass { get; set; }
        public string stuExam { get; set; }
        public int stuScore { get; set; }
    }
}
