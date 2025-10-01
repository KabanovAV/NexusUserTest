using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class UserMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта User в UserAdminDTO
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <returns>UserAdminDTO</returns>
        public static UserAdminDTO? ToAdminDto(this User entity)
            => entity == null ? null : new UserAdminDTO
            {
                Id = entity.Id,
                FullName = string.Join(" ", new[] { entity.LastName, entity.FirstName, entity.Surname }.Where(s => !string.IsNullOrEmpty(s))),
                Login = entity.Login,
                Password = entity.Password,
                Organization = entity.Organization,
                Position = entity.Position,
                GroupUserItems = entity.GroupUser != null ? [.. entity.GroupUser
                    .Select(gu => new GroupUserCreateDTO { GroupId = gu.GroupId })] : []
            };

        /// <summary>
        /// Маппинг списка из обьектов User в список UserAdminDTO
        /// </summary>
        /// <param name="entities">Список обьектов User</param>
        /// <returns>Список UserAdminDTO</returns>
        public static List<UserAdminDTO> ToAdminDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToAdminDto())];

        /// <summary>
        /// Маппинг из обьекта User в UserInfoTestDTO
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <returns>UserInfoTestDTO</returns>
        public static UserInfoTestDTO? ToTestDto(this User entity)
            => entity == null ? null : new UserInfoTestDTO
            {
                Id = entity.Id,
                FullName = string.Join(" ", new[] { entity.LastName, entity.FirstName, entity.Surname }.Where(s => !string.IsNullOrEmpty(s))),
                GroupUsers = entity.GroupUser != null ? [.. entity.GroupUser
                    .Select(gu => new GroupUserInfoTestDTO
                    {
                        Id = gu.Id,
                        GroupTitle = gu.Group != null ? gu.Group.Title : "",
                        Status = gu.Status,
                        Results = gu.Results != null ? [.. gu.Results.Select(r => new ResultInfoTestDTO{ IsCorrect = r.Answer.IsCorrect })] : []
                    })] : []
            };

        /// <summary>
        /// Маппинг списка из обьектов User в список UserInfoTestDTO
        /// </summary>
        /// <param name="entities">Список обьектов User</param>
        /// <returns>Список UserInfoTestDTO</returns>
        public static List<UserInfoTestDTO> ToTestDto(this IEnumerable<User> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToTestDto())];

        /// <summary>
        /// Маппинг из UserAdminDTO в обьект User
        /// </summary>
        /// <param name="dto">UserAdminDTO</param>
        /// <returns>User</returns>
        public static User? ToEntity(this UserAdminDTO dto)
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

        /// <summary>
        /// Маппинг списка из UserAdminDTO в список обьектов User
        /// </summary>
        /// <param name="dtos">Список UserAdminDTO</param>
        /// <returns>Список User</returns>
        public static List<User> ToEntity(this IEnumerable<UserAdminDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(e => e.ToEntity())];

        /// <summary>
        /// Маппинг обновления обьекта User
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <param name="dto">UserAdminDTO</param>
        public static void UpdateFromAdminDto(this User entity, UserAdminDTO dto)
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

        /// <summary>
        /// Обьединение и конвертирование в обьекта Question
        /// </summary>
        /// <param name="fullName">Обьект Question</param>
        /// <param name="index">QuestionDTO</param>
        private static string GetNamePart(string fullName, int index)
        {
            if (string.IsNullOrWhiteSpace(fullName))
                return string.Empty;
            var names = fullName.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            return names.Length > index ? names[index] : string.Empty;
        }

        /// <summary>
        /// Конвертирование списка из GroupUserCreateDTO в список обьектов GroupUser
        /// </summary>
        /// <param name="items">GroupUserCreateDTO</param>
        /// <returns>Список GroupUser</returns>
        private static List<GroupUser> GroupUserListConverter(List<GroupUserCreateDTO> items)
        {
            List<GroupUser> groupUser = [];
            foreach (var item in items!)
                groupUser.Add(new GroupUser { GroupId = item.GroupId, Status = item.Status });
            return groupUser;
        }

        /// <summary>
        /// Обьединение и конвертирование в обьект User
        /// </summary>
        /// <param name="entity">Обьект User</param>
        /// <param name="dto">UserAdminDTO</param>
        private static void GroupUserListConverter(this User entity, UserAdminDTO dto)
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
                        if (existing.GroupId != item.GroupId && item.GroupId != 0)
                            existing.GroupId = item.GroupId;
                        if (existing.Status != item.Status && item.Status != 1)
                            existing.Status = item.Status;
                    }
                    else
                        entity.GroupUser.Add(new GroupUser { GroupId = item.GroupId, Status = item.Status });
                }
            }
        }
    }
}
