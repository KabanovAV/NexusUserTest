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
        Task<Answer> UpdateAnswer(Answer entity, string? includeProperties = null);
        void DeleteAnswer(Answer entity);
    }

    public class AnswerRepoService : IAnswerRepoService
    {
        private readonly IRepositoryManager _repository;

        public AnswerRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Answer>> GetAllAnswerAsync(Expression<Func<Answer, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Answer.GetAllAnswerAsync(expression, includeProperties);

        public async Task<Answer> GetAnswerAsync(Expression<Func<Answer, bool>> expression, string? includeProperties = null)
            => await _repository.Answer.GetAnswerAsync(expression, includeProperties);

        public async Task<Answer> AddAnswerAsync(Answer entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Answer.AddAnswerAsync(entity);
            _repository.Save();
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
            _repository.Save();
            var questionId = entities.First().QuestionId;
            return await _repository.Answer.GetAllAnswerAsync(q => q.QuestionId == questionId, includeProperties);
        }

        public async Task<Answer> UpdateAnswer(Answer entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Answer.UpdateAnswer(entity);
            _repository.Save();
            return await _repository.Answer.GetAnswerAsync(a => a.Id == entity.Id, includeProperties);
        }

        public void DeleteAnswer(Answer entity)
        {
            _repository.Answer.DeleteAnswer(entity);
            _repository.Save();
        }
    }
}
