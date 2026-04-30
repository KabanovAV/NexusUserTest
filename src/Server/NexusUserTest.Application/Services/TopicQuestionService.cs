using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта результат тема вопрос
    /// </summary>
    public class TopicQuestionService : ITopicQuestionService
    {
        private readonly IRepository _repository;

        public TopicQuestionService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Добавить тему вопрос в набор данных
        /// </summary>
        /// <param name="entity">Тема вопрос</param>
        /// <returns>Возвращает тему вопрос после добавления в БД</returns>
        public async Task AddTopicQuestionAsync(TopicQuestion entity)
            => await _repository.TopicQuestion.AddAsync(entity);

        /// <summary>
        /// Изменить тему вопрос в наборе данных
        /// </summary>
        /// <param name="entity">Тема вопрос</param>
        public async Task UpdateTopicQuestionAsync(TopicQuestion entity)
            => await _repository.TopicQuestion.Update(entity);

        /// <summary>
        /// Удалить тему вопрос из набора данных
        /// </summary>
        /// <param name="id">Id темы вопрос</param>
        public async Task DeleteTopicQuestionAsync(int id)
            => await _repository.TopicQuestion.Remove(id);
    }
}
