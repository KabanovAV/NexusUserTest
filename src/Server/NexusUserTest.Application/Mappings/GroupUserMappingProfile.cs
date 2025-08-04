using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public class GroupUserMappingProfile : Profile
    {
        public GroupUserMappingProfile()
        {
            CreateMap<GroupUserCreateDTO, GroupUser>().ReverseMap();
        }
    }
}
