using DBConnecter.EFConnecter.DAO;
using DBConnecter.Model;

namespace ExamWebEF
{
    static public class QuestionOM
    {
        static public List<QuestionContext> GetQuestionsBaseByKey(string key)
        {
            var questions = QuestionDAO.GetQuestionsBaseByKey(key);
            return questions;
        }

        static public bool AutoQuestionToExam(Model.AutoData autoData)
        {
            if(autoData.difficultyValue=="简单")
            {
                int hard = 3;
                string subject = autoData.subject;
                int cnumber = autoData.cnumber;
                int tnumber = autoData.tnumber;
                int exam = autoData.exam;
                QuestionDAO.AutoQuestion(exam, hard, subject, cnumber, tnumber);


            }
            if (autoData.difficultyValue=="一般")
            {
                int hard = 4;
                string subject = autoData.subject;
                int cnumber = autoData.cnumber;
                int tnumber = autoData.tnumber;
                int exam = autoData.exam;
                QuestionDAO.AutoQuestion(exam, hard, subject, cnumber, tnumber);
            }

            if (autoData.difficultyValue == "困难")
            {
                int hard = 5;
                string subject = autoData.subject;
                int cnumber = autoData.cnumber;
                int tnumber = autoData.tnumber;
                int exam = autoData.exam;
                QuestionDAO.AutoQuestion(exam, hard, subject, cnumber, tnumber);
            }

            return true;
        }
    }
}
