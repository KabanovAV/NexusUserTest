namespace NexusUserTest.Domain.Common.Interfaces
{
    /// <summary>
    /// Интерфейс к операциям доступным у объектов в репозиториях
    /// </summary>
    /// <typeparam name="TEntity">Объект, представляющий таблицу из БД</typeparam>
    public interface IRepositoryOperations<TEntity> where TEntity : class
    {
        /// <summary>
        /// Полные данные объекта, включая данные других объектов через внешние связи
        /// Может быть переопределён в конкретной реализации объекта данных
        /// По умолчанию соответствует значению PlainData
        /// </summary>
        IQueryable<TEntity> Data { get; }

        /// <summary>
        /// Данные только одной таблицы.
        /// Объекты (поля) связей с другими таблицами не заполнены (null)
        /// </summary>
        IQueryable<TEntity> PlainData { get; }

        /// <summary>
        /// Получить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param>
        /// <returns>Возвращает объект из набора данных</returns>
        Task<TEntity?> GetByIdAsync(int id);

        /// <summary>
        /// Добавить один объект в набор данных
        /// </summary>
        /// <param name="entity">Добавляемый объект</param>
        /// <returns>Объект после добавления в БД</returns>
        Task<TEntity> AddAsync(TEntity entity);

        /// <summary>
        /// Добавить коллекцию объектов в набор данных
        /// </summary>
        /// <param name="entities">Добавляемые объекты</param>
        /// <returns>Объекты после добавления в БД</returns>
        Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities);

        /// <summary>
        /// Изменить один объект в наборе данных
        /// </summary>
        /// <param name="entity">Изменяемый объект</param>
        Task Update(TEntity entity);

        /// <summary>
        /// Изменить коллекцию объектов в наборе данных
        /// </summary>
        /// <param name="entities">Изменяемые объекты</param>
        Task UpdateRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param> 
        Task Remove(int id);

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="ids">Id объектов</param>
        Task RemoveRange(IEnumerable<int> ids);

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="entity">Удаляемый объект</param>
        Task Remove(TEntity entity);

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        Task RemoveRange(IEnumerable<TEntity> entities);
    }
}
