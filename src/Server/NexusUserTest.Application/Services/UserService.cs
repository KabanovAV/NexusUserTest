using NexusUserTest.Application.Common;
using NexusUserTest.Application.Common.Errors;
using NexusUserTest.Application.Common.Interfaces;
using NexusUserTest.Common.DTOs.Commands;
using NexusUserTest.Common.DTOs.Queries;
using NexusUserTest.Domain.Common.Interfaces;
using NexusUserTest.Domain.Entities;
using SibCCSPETest.WebApi.MappingProfiles;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для обьекта ответ
    /// </summary>
    public class UserService : IUserService
    {
        private readonly IRepository _repository;
        private readonly IValidationService _validationService;

        public UserService(IRepository repository, IValidationService validationService)
        {
            _repository = repository;
            _validationService = validationService;
        }

        /// <summary>
        /// Получение всех пользователей из набора данных
        /// </summary>
        /// <returns>Возвращает список пользователей из набора данных</returns>
        public async Task<Result<IEnumerable<UserDTO>>> GetAllUserAsync()
        {
            var users = await _repository.User.GetAllUserAsync();
            return users.ToDto();
        }

        /// <summary>
        /// Получение пользователя из набора данных по Id
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <returns>Возвращает пользователя из набора данных</returns>
        public async Task<Result<UserDTO>> GetUserByIdAsync(int id)
        {
            var user = await _repository.User.GetUserByIdAsync(id);
            if (user == null)
                return Result.Failure<UserDTO>(UserErrors.NotFound(id));
            return user.ToDto();
        }

        /// <summary>
        /// Добавить пользователя в набор данных
        /// </summary>
        /// <param name="createUser">Пользователь</param>
        /// <returns>Возвращает пользователя после добавления в БД</returns>
        public async Task<Result<UserDTO>> CreateUserAsync(CreateUserDTO createDto)
        {
            var validation = await _validationService.ValidateAsync(createDto);
            if (validation.IsSuccess)
            {
                var user = new User()
                {
                    Lastname = createDto.Lastname,
                    Firstname = createDto.Firstname,
                    Surname = createDto.Surname,
                    Login = createDto.Login,
                    Password = createDto.Password,
                    Organization = createDto.Organization,
                    Position = createDto.Position
                };
                await _repository.User.AddAsync(user);
                return user.ToDto();
            }
            return Result.Failure<UserDTO>(validation.Error);
        }

        /// <summary>
        /// Изменить пользователя в наборе данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        /// <param name="updateDto">Пользователь</param>
        public async Task<Result> UpdateUserAsync(int id, UpdateUserDTO updateDto)
        {
            if (id != updateDto.Id)
                return Result.Failure(UserErrors.Conflict(id, updateDto.Id));

            var validation = await _validationService.ValidateAsync(updateDto);
            if (validation.IsSuccess)
            {
                var user = await _repository.User.GetUserByIdAsync(id);
                if (user == null)
                    return Result.Failure(UserErrors.NotFound(id));
                user.UpdateFromDto(updateDto);
                await _repository.User.Update(user);
                return Result.Success();
            }
            return Result.Failure(validation.Error);
        }

        /// <summary>
        /// Удалить пользователя из набора данных
        /// </summary>
        /// <param name="id">Id пользователя</param>
        public async Task<Result> DeleteUserAsync(int id)
        {
            var user = await _repository.User.GetUserByIdAsync(id);
            if (user == null)
                return Result.Failure(UserErrors.NotFound(id));
            await _repository.User.Remove(id);
            return Result.Success();
        }
    }
}
