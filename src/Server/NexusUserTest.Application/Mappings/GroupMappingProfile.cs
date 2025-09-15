using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class GroupMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Group в GroupDTO
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>GroupDTO</returns>
        public static GroupDTO? ToDto(this Group entity)
            => entity == null ? null : new GroupDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                SpecializationId = entity.SpecializationId,
                SpecializationTitle = entity.Specialization != null ? entity.Specialization.Title : "",
                Begin = entity.Begin,
                End = entity.End
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupDTO</returns>
        public static IEnumerable<GroupDTO?> ToDto(this IEnumerable<Group> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        /// <summary>
        /// Маппинг из GroupDTO в обьект Group
        /// </summary>
        /// <param name="dto">GroupDTO</param>
        /// <returns>Обьект Group</returns>
        public static Group? ToEntity(this GroupDTO dto)
            => dto == null ? null : new Group
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

        /// <summary>
        /// Маппинг списка из GroupDTO в список обьектов Group
        /// </summary>
        /// <param name="dtos">Список GroupDTO</param>
        /// <returns>Список обьектов Group</returns>
        public static IEnumerable<Group?> ToEntity(this IEnumerable<GroupDTO> dtos)
            => dtos.Select(e => e.ToEntity()) ?? [];

        /// <summary>
        /// Маппинг из GroupCreateDTO создание в обьект Group
        /// </summary>
        /// <param name="dto">GroupCreateDTO</param>
        /// <returns>Group</returns>
        public static Group? ToEntity(this GroupCreateDTO dto)
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
        /// <param name="dto">GroupDTO</param>
        public static void UpdateFromDto(this Group entity, GroupDTO dto)
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
