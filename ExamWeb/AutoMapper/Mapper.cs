using AutoMapper;
using ExamWebEF;
using ExamWeb.DTO;
using DBConnecter.Model;

namespace ExamWeb.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Exam, DExam>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.ExamName, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.ExamDate, opt => opt.MapFrom(src => src.Date))
                .ForMember(dest => dest.ExamScore, opt => opt.MapFrom(src => src.Score))
                .ForMember(dest => dest.TimeLong, opt => opt.MapFrom(src => src.Time));
            //CreateMap<DExam, Exam>();
            CreateMap<QuestionContext, QuestionBase>()
                .ForMember(dest => dest.Stem, opt => opt.MapFrom(src => src.Stem))
                .ForMember(dest => dest.Subject, opt => opt.MapFrom(src => src.Subject))
                .ForMember(dest => dest.DifficultyLevel, opt => opt.MapFrom(src => src.DifficultyLevel))
                .ForMember(dest => dest.Cost, opt => opt.MapFrom(src => src.Cost))
                .ForMember(dest => dest.QuestionType, opt => opt.MapFrom(src => src.QuestionChoiceId != null ? "选择" : "文本"))
                .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id));
            CreateMap<AutoData, Model.AutoData>();
        }
    }
}
