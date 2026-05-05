using Microsoft.EntityFrameworkCore;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные для репозитория результат
    /// </summary>
    public class TestResultRepository(ApplicationDbContext db) : RepositoryOperations<TestResult>(db), ITestResultRepository
    {
        /// <summary>
        /// Получение всех результатов из набора данных
        /// </summary>
        /// <returns>Возвращает список результатов из набора данных</returns>
        public async Task<IEnumerable<TestResult>> GetAllResultAsync()
            => await PlainData.ToListAsync();

        /// <summary>
        /// Получение одного результата из набора данных
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <returns>Возвращает результат из набора данных</returns>
        public async Task<TestResult?> GetResultByIdAsync(int id)
            => await GetByIdAsync(id);
    }
}
