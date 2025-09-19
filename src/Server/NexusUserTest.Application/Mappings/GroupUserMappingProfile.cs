using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class GroupUserMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта GroupUser в GroupUserDTO
        /// </summary>
        /// <param name="entity">Обьект GroupUser</param>
        /// <returns>GroupUserDTO</returns>
        public static GroupUserDTO? ToDto(this GroupUser entity)
            => entity == null ? null : new GroupUserDTO
            {
                Id = entity.Id,
                Status = entity.Status,
                User = new UserInfoDTO 
                {
                    Id = entity.Id,
                    FullName = entity.User != null ?
                        string.Join(" ", new[] { entity.User.LastName, entity.User.FirstName, entity.User.Surname }.Where(s => !string.IsNullOrEmpty(s))) : "",
                    Login = entity.User != null ? entity.User.Login : "",
                    Organization = entity.User != null ? entity.User.Organization : ""
                }
            };

        /// <summary>
        /// Маппинг списка из обьектов GroupUser в список GroupUserDTO
        /// </summary>
        /// <param name="entities">Список GroupUser</param>
        /// <returns>Список GroupUser</returns>
        public static IEnumerable<GroupUserDTO?> ToDto(this IEnumerable<GroupUser> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        /// <summary>
        /// Маппинг обновления обьекта GroupUser
        /// </summary>
        /// <param name="entity">Обьект GroupUser</param>
        /// <param name="dto">GroupUserDTO</param>
        public static void UpdateFromDto(this GroupUser entity, GroupUserDTO dto)
        {
            if (dto == null) return;
            if (entity.Status != dto.Status)
                entity.Status = dto.Status;
        }
    }
}
