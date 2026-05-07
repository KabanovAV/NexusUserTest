using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория ответы
    /// </summary>
    public class AnswerRepository(ApplicationDbContext db) : RepositoryOperations<Answer>(db), IAnswerRepository
    {
        /// <summary>
        /// Получение всех ответов из набора данных
        /// </summary>
        /// <returns>Возвращает список ответов из набора данных</returns>
        public async Task<IEnumerable<Answer>> GetAllAnswerAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение ответа из набора данных
        /// </summary>
        /// <param name="id">Id ответа</param>
        /// <returns>Возвращает ответ из набора данных</returns>
        public async Task<Answer?> GetAnswerByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
