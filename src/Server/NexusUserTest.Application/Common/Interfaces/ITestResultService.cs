using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта результат
    /// </summary>
    public interface ITestResultService
    {
        /// <summary>
        /// Получение всех результатов из набора данных
        /// </summary>
        /// <returns>Возвращает список результатов из набора данных</returns>
        Task<IEnumerable<Result>> GetAllResultAsync();

        /// <summary>
        /// Получение результата из набора данных по Id
        /// </summary>
        /// <param name="id">Id результата</param>
        /// <returns>Возвращает результат из набора данных</returns>
        Task<Result?> GetResultByIdAsync(int id);

        /// <summary>
        /// Добавить множество результатов в набор данных
        /// </summary>
        /// <param name="entities">Список результатов</param>
        /// <returns>Возвращает список результатов после добавления в БД</returns>
        Task<IEnumerable<Result>> AddRangeResultAsync(List<Result> entities);

        /// <summary>
        /// Добавить результат в набор данных
        /// </summary>
        /// <param name="entity">Результат</param>
        /// <returns>Возвращает результат после добавления в БД</returns>
        Task<Result> AddResultAsync(Result entity);        

        /// <summary>
        /// Изменить результат в наборе данных
        /// </summary>
        /// <param name="entity">Результат</param>
        Task<Result> UpdateResultAsync(Result entity);

        /// <summary>
        /// Удалить результат из набора данных
        /// </summary>
        /// <param name="entities">Список результатов</param>
        Task DeleteRangeResultAsync(IEnumerable<Result> entities);
    }
}
