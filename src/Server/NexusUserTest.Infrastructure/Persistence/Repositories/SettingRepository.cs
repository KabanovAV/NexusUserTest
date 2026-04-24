using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория настройки теста
    /// </summary>
    public class SettingRepository(ApplicationDbContext db) : RepositoryOperations<Setting>(db), ISettingRepository
    {
        /// <summary>
        /// Получение одной настройки теста из набора данных
        /// </summary>
        /// <param name="id">Id настройки теста</param>
        /// <returns>Возвращает настройки теста из набора данных</returns>
        public async Task<Setting?> GetSettingByIdAsync(int id)
            => await GetAsync(id);
    }
}
