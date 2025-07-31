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
        Task<Result> UpdateResult(Result entity, string? includeProperties = null);
        void DeleteResult(Result entity);
    }

    public class ResultRepoService : IResultRepoService
    {
        private readonly IRepositoryManager _repository;

        public ResultRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Result>> GetAllResultAsync(Expression<Func<Result, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Result.GetAllResultAsync(expression, includeProperties);

        public async Task<Result> GetResultAsync(Expression<Func<Result, bool>> expression, string? includeProperties = null)
            => await _repository.Result.GetResultAsync(expression, includeProperties);

        public async Task<Result> AddResultAsync(Result entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Result.AddResultAsync(entity);
            _repository.Save();
            return await _repository.Result.GetResultAsync(r => r.Id == entity.Id, includeProperties);
        }

        public async Task<Result> UpdateResult(Result entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Result.UpdateResult(entity);
            return await _repository.Result.GetResultAsync(r => r.Id == entity.Id, includeProperties);
        }

        public void DeleteResult(Result entity)
        {
            _repository.Result.DeleteResult(entity);
            _repository.Save();
        }
    }
}
