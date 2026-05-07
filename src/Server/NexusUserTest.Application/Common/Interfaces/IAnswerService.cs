using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта ответ
    /// </summary>
    public interface IAnswerService
    {
        /// <summary>
        /// Получение всех ответов из набора данных
        /// </summary>
        /// <returns>Возвращает список ответов из набора данных</returns>
        Task<IEnumerable<Answer>> GetAllAnswerAsync();

        /// <summary>
        /// Получение ответа из набора данных по Id
        /// </summary>
        /// <param name="id">Id ответа</param>
        /// <returns>Возвращает ответ из набора данных</returns>
        Task<Answer?> GetAnswerByIdAsync(int id);

        /// <summary>
        /// Добавить множество ответов в набор данных
        /// </summary>
        /// <param name="entities">Список ответов</param>
        /// <returns>Возвращает список ответов после добавления в БД</returns>
        Task<IEnumerable<Answer>> AddRangeAnswerAsync(List<Answer> entities);

        /// <summary>
        /// Добавить ответ в набор данных
        /// </summary>
        /// <param name="entity">Ответ</param>
        /// <returns>Возвращает ответ после добавления в БД</returns>
        Task<Answer> AddAnswerAsync(Answer entity);

        /// <summary>
        /// Изменить ответ в наборе данных
        /// </summary>
        /// <param name="entity">Ответ</param>
        Task<Answer> UpdateAnswerAsync(Answer entity);

        /// <summary>
        /// Удалить ответ из набора данных
        /// </summary>
        /// <param name="id">Id ответа</param>
        Task DeleteAnswerAsync(int id);
    }
}
