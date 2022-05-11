using DBConnecter;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExamWebEF;
using ExamWeb.DTO;
using DBConnecter.OM;

namespace ExamWeb.Controllers.New_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
  
    public class studentController:ControllerBase
    {
        private readonly IMapper _mapper;
        public studentController(IMapper mapper)
        {
            _mapper = mapper;
        }


        [HttpGet]
        public ActionResult<List<Student>> students()
        {
            var students = StudentOM.GetStudents();
            var rStudents = new List<Student>();
            foreach (var student in students)
            {
                var rstudent = new Student
                {
                    Id = student.Id,
                    Account = student.Account,
                    Password = student.Password,
                    Name = student.Name,
                };
                if (student.userStudentClass != null)
                    rstudent.ClassName = student.userStudentClass.classes.Name;
                else
                    rstudent.ClassName = "无";
                rStudents.Add(rstudent);
            }
            return rStudents;
        }

    }
}
