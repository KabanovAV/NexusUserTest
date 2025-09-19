using NexusUserTest.Domain.Entities;
using System.Linq.Expressions;

namespace NexusUserTest.Domain.Repositories
{
    public interface ISettingRepository : IRepositoryBase<Setting>
    {
        Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null);
        Task AddSettingAsync(Setting entity);
        void UpdateSetting(Setting entity);
        void DeleteSetting(Setting entity);
    }
}
