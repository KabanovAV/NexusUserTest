using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using NexusUserTest.Application.Common;

namespace NexusUserTest.Application.Services
{
    /// <summary>
    /// Сервис с операциями для валидации объектов
    /// </summary>
    public class ValidationService : IValidationService
    {
        private readonly IServiceProvider _serviceProvider;

        public ValidationService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        /// <summary>
        /// Валидирует объект и выбрасывает исключение при ошибках
        /// </summary>  
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
        public async Task<Result<T>> ValidateAsync<T>(T instance)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
                return Result.Failure<T>(Error.Failure("Validation.MissingValidator", $"Валидатор типа '{typeof(T)}' не найден"));

            var validationResult = await validator.ValidateAsync(instance);
            if (!validationResult.IsValid)
            {
                return Result<T>.ValidationFailure(Error.Validation("User.Validation", "Ошибка валидации входных данных", validationResult));
            }
            return Result<T>.Success(instance);
        }

        /// <summary>
        /// Валидирует объект и возвращает результат
        /// </summary>
        /// <typeparam name="T">Тип обьекта</typeparam>
        /// <param name="instance">Обьект валидации</param>
        public async Task<ValidationResult> ValidateResultAsync<T>(T instance)
        {
            var validator = _serviceProvider.GetService<IValidator<T>>();
            if (validator == null)
                return new ValidationResult();
            return await validator.ValidateAsync(instance);
        }
    }
}
