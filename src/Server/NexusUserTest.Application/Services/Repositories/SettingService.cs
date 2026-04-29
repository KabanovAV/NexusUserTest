using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта настроки
    /// </summary>
    public class SettingService : ISettingService
    {
        private readonly IRepository _repository;

        public SettingService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение настройки из набора данных по Id
        /// </summary>
        /// <param name="id">Id настройки</param>
        /// <returns>Возвращает настройку из набора данных</returns>
        public async Task<Setting?> GetSettingByIdAsync(int id)
            => await _repository.Setting.GetSettingByIdAsync(id);

        /// <summary>
        /// Добавить настройки в набор данных
        /// </summary>
        /// <param name="entity">Настройка</param>
        /// <returns>Возвращает настройку после добавления в БД</returns>
        public async Task<Setting> AddSettingAsync(Setting entity)
        {
            await _repository.Setting.AddAsync(entity);
            return await _repository.Setting.GetSettingByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить настройки в наборе данных
        /// </summary>
        /// <param name="entity">Настройка</param>
        public async Task<Setting> UpdateSettingAsync(Setting entity)
        {
            await _repository.Setting.Update(entity);
            return await _repository.Setting.GetSettingByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить настройку из набора данных
        /// </summary>
        /// <param name="id">Id настройки</param>
        public async Task DeleteSettingAsync(int id)
            => await _repository.Setting.Delete(id);
    }
}
