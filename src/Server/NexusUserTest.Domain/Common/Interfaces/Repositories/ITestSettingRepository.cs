using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория настройки теста
    /// </summary>
    public interface ITestSettingRepository : IRepositoryOperations<TestSetting>
    {
        /// <summary>
        /// Получение настройки теста из набора данных
        /// </summary>
        /// <param name="id">Id настройки</param>
        /// <returns>Возвращает настройки теста из набора данных</returns>
        Task<TestSetting?> GetSettingByIdAsync(int id);
    }
}
