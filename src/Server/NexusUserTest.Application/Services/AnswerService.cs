using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class AnswerService : IAnswerService
    {
        private readonly IRepository _repository;

        public AnswerService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех ответов из набора данных
        /// </summary>
        /// <returns>Возвращает список ответов из набора данных</returns>
        public async Task<IEnumerable<Answer>> GetAllAnswerAsync()
            => await _repository.Answer.GetAllAnswerAsync();

        /// <summary>
        /// Получение ответа из набора данных по Id
        /// </summary>
        /// <param name="id">Id ответа</param>
        /// <returns>Возвращает ответ из набора данных</returns>
        public async Task<Answer?> GetAnswerByIdAsync(int id)
            => await _repository.Answer.GetAnswerByIdAsync(id);

        /// <summary>
        /// Добавить множество ответов в набор данных
        /// </summary>
        /// <param name="entities">Список ответов</param>
        /// <returns>Возвращает список ответов после добавления в БД</returns>
        public async Task<IEnumerable<Answer>> AddRangeAnswerAsync(List<Answer> entities)
        {
            await _repository.Answer.AddRangeAsync(entities);
            return await _repository.Answer.GetAllAnswerAsync();
        }

        /// <summary>
        /// Добавить ответ в набор данных
        /// </summary>
        /// <param name="entity">Ответ</param>
        /// <returns>Возвращает ответ после добавления в БД</returns>
        public async Task<Answer> AddAnswerAsync(Answer entity)
        {
            await _repository.Answer.AddAsync(entity);
            return await _repository.Answer.GetAnswerByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить ответ в наборе данных
        /// </summary>
        /// <param name="entity">Ответ</param>
        public async Task<Answer> UpdateAnswerAsync(Answer entity)
        {
            await _repository.Answer.Update(entity);
            return await _repository.Answer.GetAnswerByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить ответ из набора данных
        /// </summary>
        /// <param name="id">Id ответа</param>
        public async Task DeleteAnswerAsync(int id)
            => await _repository.Answer.Remove(id);
    }
}
