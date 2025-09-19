using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Infrastructure
{
    public class SettingRepository : RepositoryBase<Setting>, ISettingRepository
    {
        public SettingRepository(DbDataContext db)
            : base(db) { }

        public async Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null)
            => await GetAsync(expression, includeProperties);

        public async Task AddSettingAsync(Setting entity) => await AddAsync(entity);
        public void UpdateSetting(Setting entity) => Update(entity);
        public void DeleteSetting(Setting entity) => Delete(entity);
    }
}
