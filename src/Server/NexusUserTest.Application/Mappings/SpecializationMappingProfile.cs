using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public class SpecializationMappingProfile : Profile
    {
        public SpecializationMappingProfile()
        {
            CreateMap<Specialization, SpecializationDTO>().ReverseMap();
            CreateMap<SpecializationCreateDTO, Specialization>();
        }
    }
}
