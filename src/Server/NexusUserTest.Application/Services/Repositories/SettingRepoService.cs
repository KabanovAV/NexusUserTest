using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface ISettingRepoService
    {
        Task<IEnumerable<Setting>> GetAllSettingAsync(Expression<Func<Setting, bool>>? expression = null, string? includeProperties = null);
        Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null);
        Task<Setting> AddSettingAsync(Setting entity, string? includeProperties = null);
        Task<Setting> UpdateSetting(Setting entity, string? includeProperties = null);
        void DeleteSetting(Setting entity);
    }

    public class SettingRepoService : ISettingRepoService
    {
        private readonly IRepositoryManager _repository;

        public SettingRepoService(IRepositoryManager repository)
            => _repository = repository;

        public async Task<IEnumerable<Setting>> GetAllSettingAsync(Expression<Func<Setting, bool>>? expression = null, string? includeProperties = null)
            => await _repository.Setting.GetAllSettingAsync(expression, includeProperties);

        public async Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null)
            => await _repository.Setting.GetSettingAsync(expression, includeProperties);

        public async Task<Setting> AddSettingAsync(Setting entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Setting.AddSettingAsync(entity);
            _repository.Save();
            return await _repository.Setting.GetSettingAsync(s => s.Id == entity.Id, includeProperties);
        }

        public async Task<Setting> UpdateSetting(Setting entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Setting.UpdateSetting(entity);
            _repository.Save();
            return await _repository.Setting.GetSettingAsync(s => s.Id == entity.Id, includeProperties);
        }

        public void DeleteSetting(Setting entity)
        {
            _repository.Setting.DeleteSetting(entity);
            _repository.Save();
        }
    }
}
