using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория настройки теста
    /// </summary>
    public interface ISettingRepository : IRepositoryOperations<Setting>
    {
        /// <summary>
        /// Получение настройки теста из набора данных
        /// </summary>
        /// <param name="id">Id настройки</param>
        /// <returns>Возвращает настройки теста из набора данных</returns>
        Task<Setting?> GetSettingByIdAsync(int id);
    }
}
