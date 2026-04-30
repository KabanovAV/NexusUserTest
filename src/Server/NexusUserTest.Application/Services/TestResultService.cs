//using NexusUserTest.Application.Common;
//using NexusUserTest.Domain.Common;
//using NexusUserTest.Domain.Entities;

//namespace NexusUserTest.Application.Services
//{
//    /// <summary>
//    /// Сервис с операциями для обьекта результат
//    /// </summary>
//    public class TestResultService : ITestResultService
//    {
//        private readonly IRepository _repository;

//        public TestResultService(IRepository repository)
//        {
//            _repository = repository;
//        }

//        /// <summary>
//        /// Получение всех результатов из набора данных
//        /// </summary>
//        /// <returns>Возвращает список результатов из набора данных</returns>
//        public async Task<IEnumerable<Result>> GetAllResultAsync()
//            => await _repository.Result.GetAllResultAsync();

//        /// <summary>
//        /// Получение результата из набора данных по Id
//        /// </summary>
//        /// <param name="id">Id результата</param>
//        /// <returns>Возвращает результат из набора данных</returns>
//        public async Task<Result?> GetResultByIdAsync(int id)
//            => await _repository.Result.GetResultByIdAsync(id);

//        /// <summary>
//        /// Добавить множество результатов в набор данных
//        /// </summary>
//        /// <param name="entities">Список результатов</param>
//        /// <returns>Возвращает список результатов после добавления в БД</returns>        
//        public async Task<IEnumerable<Result>> AddRangeResultAsync(List<Result> entities)
//        {
//            await _repository.Result.AddRangeAsync(entities);
//            return await _repository.Result.GetAllResultAsync();
//            //return await _repository.Result.GetAllResultAsync(r => r.GroupUserId == entities.First().GroupUserId);
//        }

//        /// <summary>
//        /// Добавить результат в набор данных
//        /// </summary>
//        /// <param name="entity">Результат</param>
//        /// <returns>Возвращает результат после добавления в БД</returns>
//        public async Task<Result> AddResultAsync(Result entity)
//        {
//            await _repository.Result.AddAsync(entity);
//            return await _repository.Result.GetResultByIdAsync(entity.Id);
//        }

//        /// <summary>
//        /// Изменить результат в наборе данных
//        /// </summary>
//        /// <param name="entity">Результат</param>
//        public async Task<Result> UpdateResultAsync(Result entity)
//        {
//            await _repository.Result.Update(entity);
//            return await _repository.Result.GetResultByIdAsync(entity.Id);
//        }

//        /// <summary>
//        /// Удалить результат из набора данных
//        /// </summary>
//        /// <param name="entities">Список результатов</param>
//        public async Task DeleteRangeResultAsync(IEnumerable<Result> entities)
//            => await _repository.Result.DeleteRange(entities);
//    }
//}
