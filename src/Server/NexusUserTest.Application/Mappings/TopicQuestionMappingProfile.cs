using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public class TopicQuestionMappingProfile : Profile
    {
        public TopicQuestionMappingProfile()
        {
            CreateMap<TopicQuestionCreateDTO, TopicQuestion>().ReverseMap();
        }
    }
}
