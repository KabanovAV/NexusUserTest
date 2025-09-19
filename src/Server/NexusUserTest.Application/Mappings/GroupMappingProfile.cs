using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class GroupMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Group в GroupInfoDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupInfoDTO</returns>
        public static GroupInfoDTO? ToInfoDto(this Group entity)
            => entity == null ? null : new GroupInfoDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                CountOfUsers = entity.GroupUser != null ? entity.GroupUser.Count : 0,
                CountOfQuestions = entity.Specialization != null && entity.Specialization.Topics != null
                    ? entity.Specialization.Topics.Sum(t => t.TopicQuestion?.Count ?? 0) : 0
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupInfoDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupInfoDTO</returns>
        public static IEnumerable<GroupInfoDTO?> ToInfoDto(this IEnumerable<Group> entities)
            => entities.Select(e => e.ToInfoDto()) ?? [];

        /// <summary>
        /// Маппинг из обьекта Group в GroupInfoDetailsDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupInfoDetailsDTO</returns>
        public static GroupInfoDetailsDTO? ToInfoDetailsDto(this Group entity)
            => entity == null ? null : new GroupInfoDetailsDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                CountOfUsers = entity.GroupUser != null ? entity.GroupUser.Count : 0,
                CountOfQuestions = entity.Specialization != null && entity.Specialization.Topics != null
                    ? entity.Specialization.Topics.Sum(t => t.TopicQuestion?.Count ?? 0) : 0,
                Setting = new SettingDTO
                {
                    Id = entity.Setting != null ? entity.Setting.Id : 0,
                    GroupId = entity.Setting != null ? entity.Setting.GroupId : 0,
                    CountOfQuestion = entity.Setting != null ? entity.Setting.CountOfQuestion : 0,
                    Timer = entity.Setting != null ? entity.Setting.Timer : new TimeSpan()
                },
                User = entity.GroupUser.Select(gu => new GroupUserDTO
                {
                    Id = gu.Id,
                    Status = gu.Status,
                    User = new UserInfoDTO
                    {
                        Id = gu.User.Id,
                        FullName = gu.User != null ?
                        string.Join(" ", new[] { gu.User.LastName, gu.User.FirstName, gu.User.Surname }.Where(s => !string.IsNullOrEmpty(s))) : "",
                        Login = gu.User != null ? gu.User.Login : "",
                        Organization = gu.User != null ? gu.User.Organization : ""
                    }
                }).ToList()
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupInfoDetailsDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupInfoDetailsDTO</returns>
        public static IEnumerable<GroupInfoDetailsDTO?> ToInfoDetailsDto(this IEnumerable<Group> entities)
            => entities.Select(e => e.ToInfoDetailsDto()) ?? [];

        /// <summary>
        /// Маппинг из обьекта Group в GroupEditDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupEditDTO</returns>
        public static GroupEditDTO? ToEditDto(this Group entity)
            => entity == null ? null : new GroupEditDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                SpecializationId = entity.SpecializationId,
                SpecializationTitle = entity.Specialization != null ? entity.Specialization.Title : "",
                Begin = entity.Begin,
                End = entity.End
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupEditDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupEditDTO</returns>
        public static IEnumerable<GroupEditDTO?> ToEditDto(this IEnumerable<Group> entities)
            => entities.Select(e => e.ToEditDto()) ?? [];

        /// <summary>
        /// Маппинг из GroupEditDTO в обьект Group
        /// </summary>
        /// <param name="dto">GroupEditDTO</param>
        /// <returns>Обьект Group</returns>
        public static Group? ToEntity(this GroupEditDTO dto)
            => dto == null ? null : new Group
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

        /// <summary>
        /// Маппинг списка из GroupEditDTO в список обьектов Group
        /// </summary>
        /// <param name="dtos">Список GroupEditDTO</param>
        /// <returns>Список обьектов Group</returns>
        public static IEnumerable<Group?> ToEntity(this IEnumerable<GroupEditDTO> dtos)
            => dtos.Select(e => e.ToEntity()) ?? [];

        /// <summary>
        /// Маппинг из GroupEditCreateDTO создание в обьект Group
        /// </summary>
        /// <param name="dto">GroupEditCreateDTO</param>
        /// <returns>Group</returns>
        public static Group? ToEntity(this GroupEditCreateDTO dto)
            => dto == null ? null : new Group
            {
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

        /// <summary>
        /// Маппинг обновления обьекта Group
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <param name="dto">GroupEditDTO</param>
        public static void UpdateFromEditDto(this Group entity, GroupEditDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.SpecializationId != 0 && entity.SpecializationId != dto.SpecializationId)
                entity.SpecializationId = dto.SpecializationId;
            if (entity.Begin != dto.Begin)
                entity.Begin = dto.Begin;
            if (entity.End != dto.End)
                entity.End = dto.End;
        }
    }
}
