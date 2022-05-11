using DBConnecter.EFConnecter.DAO;

namespace ExamWebEF
{
    public static  class ExamOM
    {
        static public List<Exam> GetExamsByTeacher(string account)
        {
            var exams = ExamDAO.GetExamByTeacher(account);
            return exams;
        }

        static public Exam GetExamsById(int id)
        {
            var exam = ExamDAO.GetExamById(id);
            return exam;
        }
    }
}