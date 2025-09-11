using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class SpecializationMappingProfile
    {
        public static SpecializationDTO? ToDto(this Specialization entity)
            => entity == null ? null : new SpecializationDTO
            {
                Id = entity.Id,
                Title = entity.Title
            };

        public static IEnumerable<SpecializationDTO?> ToDto(this IEnumerable<Specialization> entities)
            => entities.Select(e => e.ToDto()) ?? [];

        public static Specialization? ToEntity(this SpecializationDTO dto)
            => dto == null ? null : new Specialization
            {
                Id = dto.Id,
                Title = dto.Title
            };

        public static IEnumerable<Specialization?> ToEntity(this IEnumerable<SpecializationDTO> entities)
            => entities.Select(e => e.ToEntity()) ?? [];

        public static Specialization? ToEntity(this SpecializationCreateDTO dto)
            => dto == null ? null : new Specialization
            {
                Title = dto.Title
            };

        public static void UpdateFromDto(this Specialization entity, SpecializationDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
        }
    }
}
