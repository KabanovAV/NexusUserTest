using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface IResultRepoService
    {
        Task<IEnumerable<Result>> GetAllResultAsync(Expression<Func<Result, bool>>? expression = null, string? includeProperties = null);
        Task<Result> GetResultAsync(Expression<Func<Result, bool>> expression, string? includeProperties = null);
        Task<Result> AddResultAsync(Result entity, string? includeProperties = null);
        Task<IEnumerable<Result>> AddRangeResultAsync(List<Result> entities, string? includeProperties = null);
        Task<Result> UpdateResultAsync(Result entity, string? includeProperties = null);
        Task DeleteResultAsync(IEnumerable<Result> entity);
    }

    public class ResultRepoService(IRepositoryManager repository) : IResultRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<IEnumerable<Result>> GetAllResultAsync(Expression<Func<Result, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Result.GetAllResultAsync(expression, includeProperties);

        public async Task<Result> GetResultAsync(Expression<Func<Result, bool>> expression, string? includeProperties = null)
            => await _repository.Result.GetResultAsync(expression, includeProperties);

        public async Task<Result> AddResultAsync(Result entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Result.AddResultAsync(entity);
            await _repository.SaveAsync();
            return await _repository.Result.GetResultAsync(r => r.Id == entity.Id, includeProperties);
        }

        public async Task<IEnumerable<Result>> AddRangeResultAsync(List<Result> entities, string? includeProperties = null)
        {
            foreach (var entity in entities)
            {
                entity.CreatedDate = DateTime.Now;
                entity.ChangedDate = DateTime.Now;
            }
            await _repository.Result.AddRangeResultAsync(entities);
            await _repository.SaveAsync();
            return await _repository.Result.GetAllResultAsync(r => r.GroupUserId == entities.First().GroupUserId, includeProperties);
        }

        public async Task<Result> UpdateResultAsync(Result entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Result.UpdateResult(entity);
            await _repository.SaveAsync();
            return await _repository.Result.GetResultAsync(r => r.Id == entity.Id, includeProperties);
        }

        public async Task DeleteResultAsync(IEnumerable<Result> entity)
        {
            _repository.Result.DeleteResult(entity);
            await _repository.SaveAsync();
        }
    }
}
