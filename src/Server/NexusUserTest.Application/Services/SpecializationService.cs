using NexusUserTest.Application.Common;
using NexusUserTest.Application.Mappings;
using NexusUserTest.Common;
using NexusUserTest.Common.DTOs;
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
        private readonly IValidationService _validationService;

        public SpecializationService(IRepository repository, IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        /// <summary>
        /// Получение всех специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        public async Task<Result<IEnumerable<SpecializationDTO>>> GetAllSpecializationAsync()
        {
            var specializations = await _repository.Specialization.GetAllSpecializationAsync();
            return specializations.ToDto();
        }        

        /// <summary>
        /// Получение специализации из набора данных по Id
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <returns>Возвращает специализацию из набора данных</returns>
        public async Task<Result<SpecializationDTO>> GetSpecializationByIdAsync(int id)
        {
            var specialization = await _repository.Specialization.GetSpecializationByIdAsync(id);
            if (specialization == null)
                return Result.Failure<SpecializationDTO>(SpecializationErrors.NotFound(id));
            return specialization.ToDto();
        }

        /// <summary>
        /// Получение выпадающего списка специализаций из набора данных
        /// </summary>
        /// <returns>Возвращает список специализаций из набора данных</returns>
        public async Task<Result<IEnumerable<SelectItem>>> GetSelectSpecializationAsync()
        {
            var specializations = await _repository.Specialization.GetAllSpecializationAsync();
            return specializations.ToSelect();
        }

        /// <summary>
        /// Добавить специализацию в набор данных
        /// </summary>
        /// <param name="createDto">Специализация</param>
        /// <returns>Возвращает специализацию после добавления в БД</returns>
        public async Task<Result<SpecializationDTO>> CreateSpecializationAsync(CreateSpecializationDTO createDto)
        {
            var validation = await _validationService.ValidateAsync(createDto);
            if (validation.IsSuccess)
            {
                var specialization = new Specialization() { Title = createDto.Title };
                await _repository.Specialization.AddAsync(specialization);
                return specialization.ToDto();
            }
            return Result.Failure<SpecializationDTO>(validation.Error);
        }

        /// <summary>
        /// Изменить специализацию в наборе данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        /// <param name="updateDto">Специализация</param>
        public async Task<Result> UpdateSpecializationAsync(int id, UpdateSpecializationDTO updateDto)
        {
            if (id != updateDto.Id)
                return Result.Failure<SpecializationDTO>(SpecializationErrors.Conflict(id, updateDto.Id));

            var validation = await _validationService.ValidateAsync(updateDto);
            if (validation.IsSuccess)
            {
                var specialization = await _repository.Specialization.GetSpecializationByIdAsync(id);
                if (specialization == null)
                    return Result.Failure<SpecializationDTO>(SpecializationErrors.NotFound(id));
                specialization.UpdateFromDto(updateDto);
                await _repository.Specialization.Update(specialization);
                return Result.Success();
            }
            return Result.Failure(validation.Error);
        }

        /// <summary>
        /// Удалить специализацию из набора данных
        /// </summary>
        /// <param name="id">Id специализации</param>
        public async Task<Result> DeleteSpecializationAsync(int id)
        {
            var specialization = await _repository.Specialization.GetSpecializationByIdWithChildrenAsync(id);
            if (specialization == null)
                return Result.Failure(SpecializationErrors.NotFound(id));
            if (specialization.Groups != null && specialization.Groups.Count == 0
                && specialization.Topics != null && specialization.Topics.Count == 0)
            {
                await _repository.Specialization.Remove(id);
            }
            return Result.Failure(SpecializationErrors.Connection(id));
        }
    }
}
