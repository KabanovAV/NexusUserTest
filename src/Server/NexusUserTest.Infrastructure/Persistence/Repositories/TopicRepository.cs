using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория темы
    /// </summary>
    internal class TopicRepository(ApplicationDbContext db) : RepositoryOperations<Topic>(db), ITopicRepository
    {
        /// <summary>
        /// Получение всех тем из набора данных
        /// </summary>
        /// <returns>Возвращает список тем из набора данных</returns>
        public async Task<IEnumerable<Topic>> GetAllTopicAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одной темы из набора данных
        /// </summary>
        /// <param name="id">Id темы</param>
        /// <returns>Возвращает тему из набора данных</returns>
        public async Task<Topic?> GetTopicByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
