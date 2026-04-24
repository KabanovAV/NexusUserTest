using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория результат
    /// </summary>
    public class ResultRepository(ApplicationDbContext db) : RepositoryOperations<Result>(db), IResultRepository
    {
        /// <summary>
        /// Получение всех результатов из набора данных
        /// </summary>
        /// <returns>Возвращает список результатов из набора данных</returns>
        public async Task<IEnumerable<Result>> GetAllResultAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одного результата из набора данных
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <returns>Возвращает результат из набора данных</returns>
        public async Task<Result?> GetResultByIdAsync(int id)
            => await GetAsync(id);
    }
}
