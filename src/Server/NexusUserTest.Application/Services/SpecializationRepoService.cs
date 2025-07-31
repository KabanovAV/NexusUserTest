using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface ISpecializationRepoService
    {
        Task<IEnumerable<Specialization>> GetAllSpecializationAsync(Expression<Func<Specialization, bool>>? expression = null, string? includeProperties = null);
        Task<Specialization> GetSpecializationAsync(Expression<Func<Specialization, bool>> expression, string? includeProperties = null);
        Task<Specialization> AddSpecializationAsync(Specialization entity, string? includeProperties = null);
        Task<Specialization> UpdateSpecialization(Specialization entity, string? includeProperties = null);
        bool DeleteSpecialization(Specialization entity);
    }

    public class SpecializationRepoService : ISpecializationRepoService
    {
        private readonly IRepositoryManager _repository;

        public SpecializationRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Specialization>> GetAllSpecializationAsync(Expression<Func<Specialization, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Specialization.GetAllSpecializationAsync(expression, includeProperties);

        public async Task<Specialization> GetSpecializationAsync(Expression<Func<Specialization, bool>> expression, string? includeProperties = null)
            => await _repository.Specialization.GetSpecializationAsync(expression, includeProperties);

        public async Task<Specialization> AddSpecializationAsync(Specialization entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Specialization.AddSpecializationAsync(entity);
            _repository.Save();
            return await _repository.Specialization.GetSpecializationAsync(s => s.Id == entity.Id, includeProperties);
        }

        public async Task<Specialization> UpdateSpecialization(Specialization entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Specialization.UpdateSpecialization(entity);
            _repository.Save();
            return await _repository.Specialization.GetSpecializationAsync(s => s.Id == entity.Id, includeProperties);
        }

        public bool DeleteSpecialization(Specialization entity)
        {
            if (entity.Groups != null && entity.Groups.Count != 0
                && entity.Topics != null && entity.Topics.Count != 0)
            {
                _repository.Specialization.DeleteSpecialization(entity);
                _repository.Save();
                return true;
            }
            return false;
        }
    }
}
