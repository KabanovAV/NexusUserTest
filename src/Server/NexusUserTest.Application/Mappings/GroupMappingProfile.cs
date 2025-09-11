using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class GroupMappingProfile
    {
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

        public static IEnumerable<GroupDTO?> ToDto(this IEnumerable<Group> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static Group? ToEntity(this GroupDTO dto)
            => dto == null ? null : new Group
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

        public static IEnumerable<Group?> ToEntity(this IEnumerable<GroupDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static Group? ToEntity(this GroupCreateDTO dto)
            => dto == null ? null : new Group
            {
                Title = dto.Title,
                SpecializationId = dto.SpecializationId,
                Begin = dto.Begin,
                End = dto.End
            };

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
