using ExamWebEF;
using Microsoft.EntityFrameworkCore;

namespace DBConnecter.EFConnecter.DAO
{
    static class StudentDAO
    {
        static DbContextOptions<ExamContext> contextOptions = new DbContextOptionsBuilder<ExamContext>().UseSqlServer(
            @"Data Source=172.17.0.2,1433;Initial Catalog=EOSDB;Persist Security Info=True;User ID=SA;Password=<Admin@123456>").Options;
        static internal List<UserStudent> GetStudents()
        {
            ExamContext examContext = new ExamContext(contextOptions);
            var students = examContext.userStudents.Include(s => s.userStudentClass).ThenInclude(c => c.classes).ToList();
            var studentInfo = (from stu in examContext.userStudents
                               join stuclass in examContext.userStudentClasses on stu.Id equals stuclass.UserStudentId
                               join classinfo in examContext.classes on stuclass.ClassId equals classinfo.Id
                               select stu).ToList();
            return studentInfo;
        }
    }
}
