using ExamWebEF;
using Microsoft.EntityFrameworkCore;

namespace DBConnecter.EFConnecter.DAO
{
    static class QuestionDAO
    {
        static DbContextOptions<ExamContext> contextOptions = new DbContextOptionsBuilder<ExamContext>().
            UseSqlServer(@"Data Source=172.17.0.2,1433;Initial Catalog=EOSDB;Persist Security Info=True;User ID=SA;Password=<Admin@123456>").
            Options;
        static internal List<QuestionContext> GetQuestionsBaseByKey(string key)
        {
            ExamContext examContext = new ExamContext(contextOptions);
            var questions = examContext.QuestionContext.Where(i =>
                i.Subject.Contains(key) || i.Stem.Contains(key))
                .ToList();
            return questions;
        }

        static internal void AutoQuestion(int iexam,int hard,string subject,int cnumber,int tnumber)
        {
            ExamContext examContext = new ExamContext(contextOptions);
            var Nexam = examContext.exams.Where(i => i.Id == iexam).FirstOrDefault();

            var cquestion = examContext.QuestionContext
                .Where(i => i.QuestionChoiceId != null 
                    && i.DifficultyLevel<=hard 
                    && i.Subject.Contains(subject) )
                .ToList();
            addSomeExam(Nexam, cquestion, cnumber);

            var tquestion = examContext.QuestionContext
                .Where(i => i.QuestionChoiceId == null 
                && i.DifficultyLevel <= hard
                && i.Subject.Contains(subject))
                .ToList();
            addSomeExam(Nexam, tquestion, tnumber);
        }

        static internal void addSomeExam(Exam Nexam,List<QuestionContext> cquestion,int number)
        {
            ExamContext examContext = new ExamContext(contextOptions);
            if (cquestion != null)
            {
                Random random = new Random((int)DateTime.Now.Ticks);
                var i = number;
                List<int> index = new List<int>();
                while (i > 0)
                {
                    int rnumber = random.Next(0, cquestion.Count());
                    if (!index.Contains(rnumber))
                    {
                        index.Add(rnumber);
                        var rquestion = new Question();
                        rquestion.ExamId = Nexam.Id;
                        rquestion.QuestionContextId = cquestion[rnumber].Id;
                        rquestion.exam = Nexam;
                        rquestion.questionContext = cquestion[rnumber];
                        examContext.Attach(rquestion);
                        examContext.SaveChanges();
                        i--;
                    }
                  
                }
            }
        }
    }
}
