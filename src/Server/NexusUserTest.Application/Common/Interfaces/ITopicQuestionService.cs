using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Common
{
    /// <summary>
    /// Интерфейс сервиса операциями для обьекта тема вопрос
    /// </summary>
    public interface ITopicQuestionService
    {
        /// <summary>
        /// Добавить тему вопрос в набор данных
        /// </summary>
        /// <param name="entity">Тема вопрос</param>
        /// <returns>Возвращает тему вопрос после добавления в БД</returns>
        Task AddTopicQuestionAsync(TopicQuestion entity);

        /// <summary>
        /// Изменить тему вопрос в наборе данных
        /// </summary>
        /// <param name="entity">Тема вопрос</param>
        Task UpdateTopicQuestionAsync(TopicQuestion entity);

        /// <summary>
        /// Удалить тему вопрос из набора данных
        /// </summary>
        /// <param name="id">Id темы вопрос</param>
        Task DeleteTopicQuestionAsync(int id);
    }
}
