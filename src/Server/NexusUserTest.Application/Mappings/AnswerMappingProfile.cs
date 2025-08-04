using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public class AnswerMappingProfile : Profile
    {
        public AnswerMappingProfile()
        {
            CreateMap<Answer, AnswerDTO>().ForMember(dest => dest.QuestionTitle, opt =>
            {
                opt.PreCondition(src => src.Question != null);
                opt.MapFrom(src => src.Question!.Title);
            }).ReverseMap();
            CreateMap<AnswerCreateDTO, Answer>();
        }
    }
}
