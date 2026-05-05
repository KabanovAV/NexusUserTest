using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория настройки теста
    /// </summary>
    public class TestSettingRepository(ApplicationDbContext db) : RepositoryOperations<TestSetting>(db), ITestSettingRepository
    {
        /// <summary>
        /// Получение одной настройки теста из набора данных
        /// </summary>
        /// <param name="id">Id настройки теста</param>
        /// <returns>Возвращает настройки теста из набора данных</returns>
        public async Task<TestSetting?> GetSettingByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
