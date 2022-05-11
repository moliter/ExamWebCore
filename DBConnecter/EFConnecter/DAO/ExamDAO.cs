using ExamWebEF;
using Microsoft.EntityFrameworkCore;

namespace DBConnecter.EFConnecter.DAO
{
    static class ExamDAO
    {
        static DbContextOptions<ExamContext> contextOptions = new DbContextOptionsBuilder<ExamContext>().
            UseSqlServer(@"Data Source=172.17.0.2,1433;Initial Catalog=EOSDB;Persist Security Info=True;User ID=SA;Password=<Admin@123456>").//172.17.0.2
            Options;
        static internal  List<Exam> GetExamByTeacher(string caccount)
        {
            ExamContext examContext = new ExamContext(contextOptions);
            var exams = examContext.exams.
                Where(i => i.userTeacher.Account.Equals(caccount))
                .ToList();
            return exams;
        }

        static internal Exam GetExamById(int id)
        {
            ExamContext examContext = new ExamContext(contextOptions);
            var exam = examContext.exams
                .Where(i => i.Id == id).FirstOrDefault();
            return exam;
        }

    }
}
