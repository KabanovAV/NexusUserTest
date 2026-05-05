using NexusUserTest.Common;
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
        public static GroupDTO ToDto(this Group entity)
            => new(entity.Id, entity.Title, entity.Specialization?.Title ?? "", entity.GroupUsers?.Count ?? 0, entity.Begin, entity.End);

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupDTO</returns>
        public static List<GroupDTO> ToDto(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из обьекта Group в SelectItem
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <returns>SelectItem</returns>
        public static SelectItem ToSelect(this Group entity)
            => new(entity.Id, entity.Title);

        /// <summary>
        /// Маппинг списка из обьектов Group в список SelectItem
        /// </summary>
        /// <param name="entities">Список обьектов Group</param>
        /// <returns>Список SelectItem</returns>
        public static List<SelectItem> ToSelect(this IEnumerable<Group> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToSelect())];

        /// <summary>
        /// Маппинг обновления обьекта Group
        /// </summary>
        /// <param name="entity">Обьект Group</param>
        /// <param name="dto">GroupEditDTO</param>
        public static void UpdateFromDto(this Group entity, UpdateGroupDTO dto)
        {
            if (dto == null) return;
            if (!string.IsNullOrWhiteSpace(dto.Title) && !entity.Title.Equals(dto.Title.Trim(), StringComparison.OrdinalIgnoreCase))
                entity.Title = dto.Title;
            if (entity.SpecializationId != 0 && !entity.SpecializationId.Equals(dto.SpecializationId))
                entity.SpecializationId = dto.SpecializationId;
            if (!entity.Begin.Equals(dto.Begin))
                entity.Begin = dto.Begin;
            if (!entity.End.Equals(dto.End))
                entity.End = dto.End;
        }
    }
}
