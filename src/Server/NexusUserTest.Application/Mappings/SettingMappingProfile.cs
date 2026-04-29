using NexusUserTest.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Mappings
{
    public static class SettingMappingProfile
    {
        /// <summary>
        /// Маппинг из обьекта Setting в SettingDTO
        /// </summary>
        /// <param name="entity">Обьект Setting</param>
        /// <returns>SettingDTO</returns>
        public static SettingDTO? ToDto(this Setting entity)
            => entity == null ? null : new SettingDTO
            {
                Id = entity.Id,
                GroupId = entity.GroupId,
                CountOfQuestion = entity.CountOfQuestion,
                Timer = entity.Timer
            };

        /// <summary>
        /// Маппинг списка из обьектов Group в список GroupDTO
        /// </summary>
        /// <param name="entities">Список Group</param>
        /// <returns>Список GroupDTO</returns>
        public static List<SettingDTO> ToDto(this IEnumerable<Setting> entities)
            => [.. entities.Where(e => e != null).Select(e => e.ToDto())];

        /// <summary>
        /// Маппинг из SettingDTO в обьект Setting
        /// </summary>
        /// <param name="dto">SettingDTO</param>
        /// <returns>Обьект Setting</returns>
        public static Setting? ToEntity(this SettingDTO dto)
            => dto == null ? null : new Setting
            {
                Id = dto.Id,
                GroupId = dto.GroupId,
                CountOfQuestion = dto.CountOfQuestion,
                Timer = dto.Timer
            };

        /// <summary>
        /// Маппинг списка из SettingDTO в список обьектов Setting
        /// </summary>
        /// <param name="dtos">Список SettingDTO</param>
        /// <returns>Список обьектов Setting</returns>
        public static List<Setting> ToEntity(this IEnumerable<SettingDTO> dtos)
            => [.. dtos.Where(dto => dto != null).Select(dto => dto.ToEntity())];

        /// <summary>
        /// Маппинг из SettingCreateDTO создание в обьект Setting
        /// </summary>
        /// <param name="dto">SettingCreateDTO</param>
        /// <returns>Setting</returns>
        public static Setting? ToEntity(this SettingCreateDTO dto)
            => dto == null ? null : new Setting
            {
                GroupId = dto.GroupId,
                CountOfQuestion = dto.CountOfQuestion,
                Timer = dto.Timer
            };

        /// <summary>
        /// Маппинг обновления обьекта Setting
        /// </summary>
        /// <param name="entity">Обьект Setting</param>
        /// <param name="dto">SettingDTO</param>
        public static void UpdateFromDto(this Setting entity, SettingDTO dto)
        {
            if (dto == null) return;
            if (entity.CountOfQuestion != 0 && entity.CountOfQuestion != dto.CountOfQuestion)
                entity.CountOfQuestion = dto.CountOfQuestion;
            if (entity.Timer != dto.Timer)
                entity.Timer = dto.Timer;
        }
    }
}
