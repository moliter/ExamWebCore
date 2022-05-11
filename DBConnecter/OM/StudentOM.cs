using DBConnecter.EFConnecter.DAO;
using DBConnecter.Model;
using ExamWebEF;
namespace DBConnecter.OM
{
    static public class StudentOM
    {
        static public List<UserStudent> GetStudents()
        {
            var students = StudentDAO.GetStudents();
            return students;
        }
    }
}
