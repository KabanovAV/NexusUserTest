using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class QuestionRepository(DbDataContext db) : RepositoryBase<Question>(db), IQuestionRepository
    {
        public async Task<IEnumerable<Question>> GetAllQuestionAsync(Expression<Func<Question, bool>>? expression = null, string? includeProperties = null)
            => await GetAllAsync(expression, includeProperties);

        public async Task<Question> GetQuestionAsync(Expression<Func<Question, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddQuestionAsync(Question entity) => await AddAsync(entity);
        public void UpdateQuestion(Question entity) => Update(entity);
        public void DeleteQuestion(Question entity) => Delete(entity);
    }
}
