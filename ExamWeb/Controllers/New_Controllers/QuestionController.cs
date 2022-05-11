using DBConnecter;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using ExamWebEF;
using ExamWeb.DTO;
using DBConnecter.Model;

namespace ExamWeb.Controllers.New_Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class questionController : ControllerBase
    {
        private readonly IMapper _mapper;
        public questionController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("{key}")]
        public ActionResult<List<QuestionBase>> questionbase(string key)
        {
            var questionBases = QuestionOM.GetQuestionsBaseByKey(key);
            var rquestionBases = _mapper.Map<List<QuestionBase>>(questionBases);
            return rquestionBases;
        }

        [HttpPost]
        public IResult autoquestion([FromBody] AutoData autoData)
        {
            var mautoData = _mapper.Map<Model.AutoData>(autoData);
            bool success = QuestionOM.AutoQuestionToExam(mautoData);
            if (success)
                return Results.Ok();
            else
                return Results.BadRequest();
        }
    }
}
