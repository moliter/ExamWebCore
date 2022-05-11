using Microsoft.EntityFrameworkCore;


namespace ExamWebEF
{
    public class ExamContext : DbContext
    {
        public ExamContext(DbContextOptions<ExamContext> options) : base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Data Source=localhost,1433;Initial Catalog=EOSDB;Persist Security Info=True;User ID=SA;Password=<Admin@123456>");
        }
        public DbSet<UserStudent> userStudents { get; set; }
        public DbSet<UserTeacher> userTeachers { get; set; }
        public DbSet<Class> classes { get; set; }
        public DbSet<UserStudentClass> userStudentClasses { get; set; }
        public DbSet<UserTeacherClass> userTeacherClasses { get; set; }
        public DbSet<Exam> exams { get; set; }
        public DbSet<StudentExam> studentExams { get; set; }
        public DbSet<Question> questions { get; set; }
        public DbSet<Answer> Answer { get; set; }
        public DbSet<QuestionContext> QuestionContext { get; set; }
        public DbSet<QuestionChoice> QuestionChoice { get; set; }
        public DbSet<Score> Score { get; set; }
        public DbSet<StudentQuestionAnswer> StudentQuestionAnswers { get; set; }
    }
}
