using NexusUserTest.Domain.Entities;
using NexusUserTest.Domain.Repositories;
using System.Linq.Expressions;

namespace NexusUserTest.Application.Services
{
    public interface ISettingRepoService
    {
        Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null);
        Task<Setting> AddSettingAsync(Setting entity, string? includeProperties = null);
        Task<Setting> UpdateSettingAsync(Setting entity, string? includeProperties = null);
        Task DeleteSettingAsync(Setting entity);
    }

    public class SettingRepoService(IRepositoryManager repository) : ISettingRepoService
    {
        private readonly IRepositoryManager _repository = repository;

        public async Task<Setting> GetSettingAsync(Expression<Func<Setting, bool>> expression, string? includeProperties = null)
            => await _repository.Setting.GetSettingAsync(expression, includeProperties);

        public async Task<Setting> AddSettingAsync(Setting entity, string? includeProperties = null)
        {
            entity.CreatedDate = DateTime.Now;
            entity.ChangedDate = DateTime.Now;
            await _repository.Setting.AddSettingAsync(entity);
            await _repository.SaveAsync();
            return await _repository.Setting.GetSettingAsync(s => s.Id == entity.Id, includeProperties);
        }

        public async Task<Setting> UpdateSettingAsync(Setting entity, string? includeProperties = null)
        {
            entity.ChangedDate = DateTime.Now;
            _repository.Setting.UpdateSetting(entity);
            await _repository.SaveAsync();
            return await _repository.Setting.GetSettingAsync(s => s.Id == entity.Id, includeProperties);
        }

        public async Task DeleteSettingAsync(Setting entity)
        {
            _repository.Setting.DeleteSetting(entity);
            await _repository.SaveAsync();
        }
    }
}
