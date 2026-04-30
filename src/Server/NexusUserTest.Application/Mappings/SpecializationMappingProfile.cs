using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class SpecializationMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Specialization в SpecializationDTO
        /// </summary>
        /// <param name="entity">Обьект Specialization</param>
        /// <returns>SpecializationDTO</returns>
        public static SpecializationDTO ToDto(this Specialization entity)
            => new(entity.Id, entity.Title);

        /// <summary>
        /// Маппинг списка из обьектов Specialization в список SpecializationDTO
        /// </summary>
        /// <param name="entities">Список обьектов Specialization</param>
        /// <returns>Список SpecializationDTO</returns>
        public static List<SpecializationDTO> ToDto(this IEnumerable<Specialization> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из обьекта Specialization в SelectItem
        /// </summary>
        /// <param name="entity">Обьект Specialization</param>
        /// <returns>SelectItem</returns>
        public static SelectItem ToSelect(this Specialization entity)
            => new(entity.Id, entity.Title);

        /// <summary>
        /// Маппинг списка из обьектов Specialization в список SelectItem
        /// </summary>
        /// <param name="entities">Список обьектов Specialization</param>
        /// <returns>Список SelectItem</returns>
        public static List<SelectItem> ToSelect(this IEnumerable<Specialization> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToSelect())];
    }
}
