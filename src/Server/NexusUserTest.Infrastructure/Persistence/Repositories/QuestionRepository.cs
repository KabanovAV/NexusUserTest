using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория вопросов
    /// </summary>
    public class QuestionRepository(ApplicationDbContext db) : RepositoryOperations<Question>(db), IQuestionRepository
    {
        /// <summary>
        /// Получение всех вопросов из набора данных
        /// </summary>
        /// <returns>Возвращает список всех вопросов из набора данных</returns>
        public async Task<IEnumerable<Question>> GetAllQuestionAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одного вопроса из набора данных
        /// </summary>
        /// <param name="id">Id вопроса</param>
        /// <returns>Возвращает вопрос из набора данных</returns>
        public async Task<Question?> GetQuestionByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
