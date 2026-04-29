using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория результат
    /// </summary>
    public interface IResultRepository : IRepositoryOperations<Result>
    {
        /// <summary>
        /// Получение всех результатов из набора данных
        /// </summary>
        /// <returns>Возвращает список всех результатов из набора данных</returns>
        Task<IEnumerable<Result>> GetAllResultAsync();

        /// <summary>
        /// Получение одного результата из набора данных
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <returns>Возвращает один результат из набора данных</returns>
        Task<Result?> GetResultByIdAsync(int id);
    }
}
