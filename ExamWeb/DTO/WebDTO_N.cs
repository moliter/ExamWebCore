namespace ExamWeb.DTO
{
    public class WebDTO_N
    {
    }

    public class DExam
    {
        public int? Id { get; set; }
        public string? ExamName { get; set; }
        public string? ExamDate { get; set; }
        public float? TimeLong { get; set; }
        public int? ExamScore { get; set; }
    }

    public class Tquestion
    {
        public string istype { get; set; } //种类
        public string level { get; set; } //难度等级选中值 
        public string answer { get; set; } //正确答案
        public string question { get; set; } //题目
        public string subject { get; set; }//科目
        public string cost { get; set; }
    }

    public class Cquestion
    {
        public string subject { get; set; } //试卷名称
        public string level { get; set; }//难度等级选中值 
        public string rightAnswer { get; set; } //正确答案选中值
        public string question { get; set; } //题目
        public string answerA { get; set; }//A
        public string answerB { get; set; }//B
        public string answerC { get; set; }//C
        public string answerD { get; set; }//D
        public string istype { get; set; }//种类
        public string cost { get; set; }////分值
    }

    public class QuestionBase
    {
        public string id { get; set; }
        public string Stem { get; set; }
        public string Subject { get; set; }
        public string QuestionType { get; set; }
        public string Cost { get; set; }
        public string DifficultyLevel { get; set; }


    }

    public class AutoData
    {
        public int exam { get; set; }
        public int cnumber { get; set; }
        public int tnumber { get; set; }
        public string subject { get; set; }
        public string  difficultyValue { get; set; }
    }

    public class Student
    {
        public int Id { get; set; }
        public string? Account { get; set; }
        public string? Password { get; set; }
        public string? Name { get; set; }
        public string? ClassName { get;set; }
    }
}
