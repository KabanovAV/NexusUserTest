using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class TopicMappingProfile
    {
        public static TopicDTO? ToDto(this Topic entity)
            => entity == null ? null : new TopicDTO
            {
                Id = entity.Id,
                Title = entity.Title,
                SpecializationId = entity.SpecializationId,
                SpecializationTitle = entity.Specialization != null ? entity.Specialization.Title : ""
            };

        public static IEnumerable<TopicDTO?> ToDto(this IEnumerable<Topic> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static Topic? ToEntity(this TopicDTO dto)
            => dto == null ? null : new Topic
            {
                Id = dto.Id,
                Title = dto.Title,
                SpecializationId = dto.SpecializationId
            };

        public static IEnumerable<Topic?> ToEntity(this IEnumerable<TopicDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static Topic? ToEntity(this TopicCreateDTO dto)
            => dto == null ? null : new Topic
            {
                Title = dto.Title,
                SpecializationId = dto.SpecializationId
            };

        public static void UpdateFromDto(this Topic entity, TopicDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
            if (entity.SpecializationId != 0 && entity.SpecializationId != dto.SpecializationId)
                entity.SpecializationId = dto.SpecializationId;
        }
    }
}
