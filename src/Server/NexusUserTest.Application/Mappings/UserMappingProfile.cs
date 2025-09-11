using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class UserMappingProfile
    {
        public static UserDTO? ToDto(this User entity)
            => entity == null ? null : new UserDTO
            {
                Id = entity.Id,
                FullName = string.Join(" ", new[] { entity.LastName, entity.FirstName, entity.Surname }.Where(s => !string.IsNullOrEmpty(s))),
                Login = entity.Login,
                Password = entity.Password,
                Organization = entity.Organization,
                Position = entity.Position,
                GroupUserItems = entity.GroupUser != null ? [.. entity.GroupUser
                    .Select(gu => new GroupUserCreateDTO { GroupId = gu.GroupId, UserId = gu.UserId })] : []
            };

        public static IEnumerable<UserDTO?> ToDto(this IEnumerable<User> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static User? ToEntity(this UserDTO dto)
            => dto == null ? null : new User
            {
                Id = dto.Id,
                LastName = GetNamePart(dto.FullName, 0),
                FirstName = GetNamePart(dto.FullName, 1),
                Surname = GetNamePart(dto.FullName, 2),
                Login = dto.Login,
                Password = dto.Password,
                Organization = dto.Organization,
                Position = dto.Position,
                GroupUser = dto.GroupUserItems != null ? GroupUserListConverter(dto.GroupUserItems) : []
            };

        public static IEnumerable<User?> ToEntity(this IEnumerable<UserDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static User? ToEntity(this UserCreateDTO dto)
            => dto == null ? null : new User
            {
                LastName = GetNamePart(dto.FullName, 0),
                FirstName = GetNamePart(dto.FullName, 1),
                Surname = GetNamePart(dto.FullName, 2),
                Login = dto.Login,
                Password = dto.Password,
                Organization = dto.Organization,
                Position = dto.Position,
                GroupUser = dto.GroupUserItems != null ? GroupUserListConverter(dto.GroupUserItems) : []
            };

        public static void UpdateFromDto(this User entity, UserDTO dto)
        {
            if (dto == null) return;

            var LastName = GetNamePart(dto.FullName, 0);
            var FirstName = GetNamePart(dto.FullName, 1);
            var Surname = GetNamePart(dto.FullName, 2);

            if (LastName != null && !string.IsNullOrEmpty(LastName) && entity.LastName != LastName)
                entity.LastName = LastName;
            if (FirstName != null && !string.IsNullOrEmpty(FirstName) && entity.FirstName != FirstName)
                entity.FirstName = FirstName;
            if (Surname != null && !string.IsNullOrEmpty(Surname) && entity.Surname != Surname)
                entity.Surname = Surname;
            if (dto.Login != null && !string.IsNullOrEmpty(dto.Login) && entity.Login != dto.Login)
                entity.Login = dto.Login;
            if (dto.Password != null && !string.IsNullOrEmpty(dto.Password) && entity.Password != dto.Password)
                entity.Password = dto.Password;
            if (dto.Organization != null && !string.IsNullOrEmpty(dto.Organization) && entity.Organization != dto.Organization)
                entity.Organization = dto.Organization;
            if (dto.Position != null && !string.IsNullOrEmpty(dto.Position) && entity.Position != dto.Position)
                entity.Position = dto.Position;
            if (dto.GroupUserItems != null)
                GroupUserListConverter(entity, dto);
        }

        private static string GetNamePart(string fullName, int index)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return names.Length > index ? names[index] : string.Empty;
        }

        private static List<GroupUser> GroupUserListConverter(List<GroupUserCreateDTO> items)
        {
            List<GroupUser> groupUser = [];
            foreach (var item in items!)
                groupUser.Add(new GroupUser { GroupId = item.GroupId, UserId = item.UserId });
            return groupUser;
        }

        private static void GroupUserListConverter(this User entity, UserDTO dto)
        {
            if (entity.GroupUser != null && dto.GroupUserItems != null)
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
}
