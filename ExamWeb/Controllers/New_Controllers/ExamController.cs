using DBConnecter;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExamWebEF;
using ExamWeb.DTO;

namespace ExamWeb.Controllers.New_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class examController : ControllerBase
    {
        private readonly IMapper _mapper;
        public examController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{account}")]
        public ActionResult<List<DExam>> examsteacher([FromRoute] string account) 
        {

            var exams = ExamWebEF.ExamOM.GetExamsByTeacher(account.ToString());
            var rExams = _mapper.Map<List<DExam>>(exams);
            return rExams;
        }

        [HttpGet("{id}")]
        public ActionResult<DExam> exam(int id)
        {
            var exam = ExamWebEF.ExamOM.GetExamsById(id);
            var rExam = _mapper.Map<DExam>(exam);
            return rExam;
        }

    }
}
