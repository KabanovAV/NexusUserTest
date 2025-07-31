using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface ISpecializationRepository : IRepositoryBase<Specialization>
    {
        Task<IEnumerable<Specialization>> GetAllSpecializationAsync(Expression<Func<Specialization, bool>>? expression = null, string? includeProperties = null);
        Task<Specialization> GetSpecializationAsync(Expression<Func<Specialization, bool>> expression, string? includeProperties = null);
        Task AddSpecializationAsync(Specialization entity);
        void UpdateSpecialization(Specialization entity);
        void DeleteSpecialization(Specialization entity);
    }
}
