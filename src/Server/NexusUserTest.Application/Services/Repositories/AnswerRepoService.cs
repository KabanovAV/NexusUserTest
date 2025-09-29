using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IAnswerRepoService
    {
        Task<IEnumerable<Answer>> GetAllAnswerAsync(Expression<Func<Answer, bool>>? expression = null, string? includeProperties = null);
        Task<Answer> GetAnswerAsync(Expression<Func<Answer, bool>> expression, string? includeProperties = null);
        Task<Answer> AddAnswerAsync(Answer entity, string? includeProperties = null);
        Task<IEnumerable<Answer>> AddRangeAnswerAsync(List<Answer> entities, string? includeProperties = null);
        Task<Answer> UpdateAnswerAsync(Answer entity, string? includeProperties = null);
        Task DeleteAnswerAsync(Answer entity);
    }

    public class AnswerRepoService(IRepositoryManager repository) : IAnswerRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<IEnumerable<Answer>> GetAllAnswerAsync(Expression<Func<Answer, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Answer.GetAllAnswerAsync(expression, includeProperties);

        public async Task<Answer> GetAnswerAsync(Expression<Func<Answer, bool>> expression, string? includeProperties = null)
            => await _repository.Answer.GetAnswerAsync(expression, includeProperties);

        public async Task<Answer> AddAnswerAsync(Answer entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Answer.AddAnswerAsync(entity);
            await _repository.SaveAsync();
            return await _repository.Answer.GetAnswerAsync(a => a.Id == entity.Id, includeProperties);
        }

        public async Task<IEnumerable<Answer>> AddRangeAnswerAsync(List<Answer> entities, string? includeProperties = null)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.ChangedDate = DateTime.Now;
            }            
            await _repository.Answer.AddRangeAnswerAsync(entities);
            await _repository.SaveAsync();
            return await _repository.Answer.GetAllAnswerAsync(q => q.QuestionId == entities.First().QuestionId, includeProperties);
        }

        public async Task<Answer> UpdateAnswerAsync(Answer entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Answer.UpdateAnswer(entity);
            await _repository.SaveAsync();
            return await _repository.Answer.GetAnswerAsync(a => a.Id == entity.Id, includeProperties);
        }

        public async Task DeleteAnswerAsync(Answer entity)
        {
            _repository.Answer.DeleteAnswer(entity);
            await _repository.SaveAsync();
        }
    }
}
