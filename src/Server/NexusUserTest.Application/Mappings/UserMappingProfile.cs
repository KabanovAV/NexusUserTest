using AutoMapper;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<User, UserDTO>().ForMember(dest => dest.FullName, opt => opt.MapFrom(src =>
                string.Join(" ", new[] { src.LastName, src.FirstName, src.Surname }.Where(s => !string.IsNullOrEmpty(s)))))
                .ForMember(dest => dest.GroupUserItems, opt => opt.MapFrom(src => src.GroupUser))
                .ReverseMap()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetNamePart(src.FullName, 0)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetNamePart(src.FullName, 1)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => GetNamePart(src.FullName, 2)))
                .ForMember(dest => dest.GroupUser, opt => opt.Ignore())
                .AfterMap((dto, entity) => GroupUserListConverter(dto, entity));

            CreateMap<UserCreateDTO, User>()
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => GetNamePart(src.FullName, 0)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => GetNamePart(src.FullName, 1)))
                .ForMember(dest => dest.Surname, opt => opt.MapFrom(src => GetNamePart(src.FullName, 2)))
                .ForMember(dest => dest.GroupUser, opt => opt.MapFrom(src => src.GroupUserItems));
        }

        private static string GetNamePart(string fullName, int index)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return names.Length > index ? names[index] : string.Empty;
        }

        private static void GroupUserListConverter(UserDTO dto, User entity)
        {
            var toRemove = entity.GroupUser
                        .Where(gu => !dto.GroupUserItems.Any(dtoGu => dtoGu.GroupId == gu.GroupId))
                        .ToList();

            foreach (var item in toRemove)
                entity.GroupUser.Remove(item);

            foreach (var item in dto.GroupUserItems!)
            {
                var existing = entity.GroupUser.FirstOrDefault(gu => gu.GroupId == item.GroupId);
                if (existing != null)
                {
                    existing.GroupId = item.GroupId;
                    existing.UserId = item.UserId;
                }
                else
                    entity.GroupUser.Add(new GroupUser { GroupId = item.GroupId, UserId = item.UserId });
            }
        }
    }
}
