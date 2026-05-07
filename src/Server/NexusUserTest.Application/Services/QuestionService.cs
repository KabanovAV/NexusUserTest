using NexusUserTest.Application.Common.Interfaces;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта вопрос
    /// </summary>
    public class QuestionService : IQuestionService
    {
        private readonly IRepository _repository;

        public QuestionService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех вопросов из набора данных
        /// </summary>
        /// <returns>Возвращает список вопросов из набора данных</returns>
        public async Task<IEnumerable<Question>> GetAllQuestionAsync()
            => await _repository.Question.GetAllQuestionAsync();

        /// <summary>
        /// Получение вопроса из набора данных по Id
        /// </summary>
        /// <param name="id">Id вопроса</param>
        /// <returns>Возвращает вопрос из набора данных</returns>
        public async Task<Question?> GetQuestionByIdAsync(int id)
            => await _repository.Question.GetQuestionByIdAsync(id);

        /// <summary>
        /// Добавить вопрос в набор данных
        /// </summary>
        /// <param name="entity">Вопрос</param>
        /// <returns>Возвращает вопрос после добавления в БД</returns>
        public async Task<Question> AddQuestionAsync(Question entity)
        {
            await _repository.Question.AddAsync(entity);
            return await _repository.Question.GetQuestionByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить вопроса в наборе данных
        /// </summary>
        /// <param name="entity">Вопрос</param>
        public async Task<Question> UpdateQuestionAsync(Question entity)
        {
            await _repository.Question.Update(entity);
            return await _repository.Question.GetQuestionByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить вопрос из набора данных
        /// </summary>
        /// <param name="id">Id группы</param>
        public async Task DeleteQuestionAsync(int id)
            => await _repository.Question.Remove(id);
    }
}
