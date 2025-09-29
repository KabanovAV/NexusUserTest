using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace SibCCSPETest.WebApi.MappingProfiles
{
    public static class SpecializationMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Specialization в SpecializationDTO
        /// </summary>
        /// <param name="entity">Обьект Specialization</param>
        /// <returns>SpecializationDTO</returns>
        public static SpecializationDTO? ToAdminDto(this Specialization entity)
            => entity == null ? null : new SpecializationDTO
            {
                Id = entity.Id,
                Title = entity.Title
            };

        /// <summary>
        /// Маппинг списка из обьектов Specialization в список SpecializationDTO
        /// </summary>
        /// <param name="entities">Список обьектов Specialization</param>
        /// <returns>Список SpecializationDTO</returns>
        public static List<SpecializationDTO> ToAdminDto(this IEnumerable<Specialization> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToAdminDto())];

        /// <summary>
        /// Маппинг из SpecializationDTO в обьект Specialization
        /// </summary>
        /// <param name="dto">SpecializationDTO</param>
        /// <returns>Обьект Specialization</returns>
        public static Specialization? ToEntity(this SpecializationDTO dto)
            => dto == null ? null : new Specialization
            {
                Id = dto.Id,
                Title = dto.Title
            };

        /// <summary>
        /// Маппинг списка из SpecializationDTO в список обьектов Specialization
        /// </summary>
        /// <param name="dtos">Список SpecializationDTO</param>
        /// <returns>Список обьектов Specialization</returns>
        public static List<Specialization> ToEntity(this IEnumerable<SpecializationDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг из SpecializationCreateDTO создание в обьект Specialization
        /// </summary>
        /// <param name="dto">SpecializationCreateDTO</param>
        /// <returns>Обьект Specialization</returns>
        public static Specialization? ToEntity(this SpecializationCreateDTO dto)
            => dto == null ? null : new Specialization
            {
                Title = dto.Title
            };

        /// <summary>
        /// Маппинг обновления обьекта Specialization
        /// </summary>
        /// <param name="entity">Обьект Specialization</param>
        /// <param name="dto">SpecializationDTO</param>
        public static void UpdateFromDto(this Specialization entity, SpecializationDTO dto)
        {
            if (dto == null) return;
            if (dto.Title != null && !string.IsNullOrEmpty(dto.Title) && entity.Title != dto.Title)
                entity.Title = dto.Title;
        }
    }
}
