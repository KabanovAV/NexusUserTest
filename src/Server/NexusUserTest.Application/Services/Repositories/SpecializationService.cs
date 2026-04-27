using NexusUserTest.Application.Common;
using NexusUserTest.Domain.Common;
using NexusUserTest.Domain.Entities;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта специализации
    /// </summary>
    public class SpecializationService : ISpecializationService
    {
        private readonly IRepository _repository;

        public SpecializationService(IRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Получение всех специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        public async Task<IEnumerable<Specialization>> GetAllSpecializationAsync()
            => await _repository.Specialization.GetAllSpecializationAsync();

        /// <summary>
        /// Получение специализации из набора данных по Id
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        public async Task<Specialization?> GetSpecializationByIdAsync(int id)
            => await _repository.Specialization.GetSpecializationByIdAsync(id);

        /// <summary>
        /// Добавить специализацию в набор данных
        /// </summary>
        /// <param name="entity">Специализация</param>
        /// <returns>Возвращает специализацию после добавления в БД</returns>
        public async Task<Specialization> AddSpecializationAsync(Specialization entity)
        {
            await _repository.Specialization.AddAsync(entity);
            return await _repository.Specialization.GetSpecializationByIdAsync(entity.Id);
        }

        /// <summary>
        /// Изменить специализацию в наборе данных
        /// </summary>
        /// <param name="entity">Специализация</param>
        public async Task<Specialization> UpdateSpecializationAsync(Specialization entity)
        {
            await _repository.Specialization.Update(entity);
            return await _repository.Specialization.GetSpecializationByIdAsync(entity.Id);
        }

        /// <summary>
        /// Удалить специализацию из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        public async Task DeleteSpecializationAsync(int id)
            => await _repository.Specialization.Delete(id);
    }
}
