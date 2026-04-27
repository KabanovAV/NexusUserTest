using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта настройки
    /// </summary>
    public interface ISettingService
    {
        /// <summary>
        /// Получение настройки из набора данных по Id
        /// </summary>
        /// <param name="id">Id настройки</param>
        /// <returns>Возвращает настройку из набора данных</returns>
        Task<Setting?> GetSettingByIdAsync(int id);

        /// <summary>
        /// Добавить настройки в набор данных
        /// </summary>
        /// <param name="entity">Настройка</param>
        /// <returns>Возвращает настройку после добавления в БД</returns>
        Task<Setting> AddSettingAsync(Setting entity);

        /// <summary>
        /// Изменить настройки в наборе данных
        /// </summary>
        /// <param name="entity">Настройка</param>
        Task<Setting> UpdateSettingAsync(Setting entity);

        /// <summary>
        /// Удалить настройку из набора данных
        /// </summary>
        /// <param name="id">Id настройки</param>
        Task DeleteSettingAsync(int id);
    }
}
