using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта тема
    /// </summary>
    public interface ITopicService
    {
        /// <summary>
        /// Получение всех тем из набора данных
        /// </summary>
        /// <returns>Возвращает список тем из набора данных</returns>
        Task<IEnumerable<Topic>> GetAllTopicAsync();

        /// <summary>
        /// Получение тему из набора данных по Id
        /// </summary>
        /// <param name="id">Id темы</param>
        /// <returns>Возвращает тему из набора данных</returns>
        Task<Topic?> GetTopicByIdAsync(int id);

        /// <summary>
        /// Добавить тему в набор данных
        /// </summary>
        /// <param name="entity">Тема</param>
        /// <returns>Возвращает тему после добавления в БД</returns>
        Task<Topic> AddTopicAsync(Topic entity);

        /// <summary>
        /// Изменить темы в наборе данных
        /// </summary>
        /// <param name="entity">Результат</param>
        Task<Topic> UpdateTopicAsync(Topic entity);

        /// <summary>
        /// Удалить тему из набора данных
        /// </summary>
        /// <param name="id">Id темы</param>
        Task DeleteTopicAsync(int id);
    }
}
