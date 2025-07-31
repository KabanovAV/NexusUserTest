using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface IAnswerRepository : IRepositoryBase<Answer>
    {
        Task<IEnumerable<Answer>> GetAllAnswerAsync(Expression<Func<Answer, bool>>? expression = null, string? includeProperties = null);
        Task<Answer> GetAnswerAsync(Expression<Func<Answer, bool>> expression, string? includeProperties = null);
        Task AddAnswerAsync(Answer entity);
        Task AddRangeAnswerAsync(List<Answer> entities);
        void UpdateAnswer(Answer entity);
        void DeleteAnswer(Answer entity);
    }
}
