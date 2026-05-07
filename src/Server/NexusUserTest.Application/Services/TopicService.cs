using NexusUserTest.Application.Common.Interfaces;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта тема
    /// </summary>
    public class TopicService : ITopicService
    {
        private readonly IRepository _repository;

        public TopicService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех тем из набора данных
        /// </summary>
        /// <returns>Возвращает список тем из набора данных</returns>
        public async Task<IEnumerable<Topic>> GetAllTopicAsync()
            => await _repository.Topic.GetAllTopicAsync();

        /// <summary>
        /// Получение тему из набора данных по Id
        /// </summary>
        /// <param name="id">Id темы</param>
        /// <returns>Возвращает тему из набора данных</returns>
        public async Task<Topic?> GetTopicByIdAsync(int id)
            => await _repository.Topic.GetTopicByIdAsync(id);

        /// <summary>
        /// Добавить тему в набор данных
        /// </summary>
        /// <param name="entity">Тема</param>
        /// <returns>Возвращает тему после добавления в БД</returns>
        public async Task<Topic> AddTopicAsync(Topic entity)
        {
            await _repository.Topic.AddAsync(entity);
            return await _repository.Topic.GetTopicByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить темы в наборе данных
        /// </summary>
        /// <param name="entity">Результат</param>
        public async Task<Topic> UpdateTopicAsync(Topic entity)
        {
            await _repository.Topic.Update(entity);
            return await _repository.Topic.GetTopicByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить тему из набора данных
        /// </summary>
        /// <param name="id">Id темы</param>
        public async Task DeleteTopicAsync(int id)
            => await _repository.Topic.Remove(id);
    }
}
