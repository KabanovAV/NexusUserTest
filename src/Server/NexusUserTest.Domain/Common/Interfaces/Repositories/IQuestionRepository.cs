using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Domain.Common
{
    /// <summary>
    /// Интерфейс с операциями для репозитория вопросы
    /// </summary>
    public interface IQuestionRepository : IRepositoryOperations<Question>
    {
        /// <summary>
        /// Получение всех вопросов из набора данных
        /// </summary>
        /// <returns>Возвращает список всех вопросов из набора данных</returns>
        Task<IEnumerable<Question>> GetAllQuestionAsync();

        /// <summary>
        /// Получение одного вопроса из набора данных
        /// </summary>
        /// <param name="id">Id вопроса</param>
        /// <returns>Возвращает один вопрос из набора данных</returns>
        Task<Question> GetQuestionByIdAsync(int id);
    }
}
