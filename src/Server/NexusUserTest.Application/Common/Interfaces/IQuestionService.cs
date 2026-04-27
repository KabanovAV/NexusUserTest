using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта вопрос
    /// </summary>
    public interface IQuestionService
    {
        /// <summary>
        /// Получение всех вопросов из набора данных
        /// </summary>
        /// <returns>Возвращает список вопросов из набора данных</returns>
        Task<IEnumerable<Question>> GetAllQuestionAsync();

        /// <summary>
        /// Получение вопроса из набора данных по Id
        /// </summary>
        /// <param name="id">Id вопроса</param>
        /// <returns>Возвращает вопрос из набора данных</returns>
        Task<Question?> GetQuestionByIdAsync(int id);

        /// <summary>
        /// Добавить вопрос в набор данных
        /// </summary>
        /// <param name="entity">Вопрос</param>
        /// <returns>Возвращает вопрос после добавления в БД</returns>
        Task<Question> AddQuestionAsync(Question entity);

        /// <summary>
        /// Изменить вопроса в наборе данных
        /// </summary>
        /// <param name="entity">Вопрос</param>
        Task<Question> UpdateQuestionAsync(Question entity);

        /// <summary>
        /// Удалить вопрос из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        Task DeleteQuestionAsync(int id);
    }
}
