using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using NexusUserTest.Domain.Common;

namespace NexusUserTest.Infrastructure
{
    /// <summary>
    /// Базовые операциии доступные у объектов в репозиториях
    /// </summary>
    /// <typeparam name="TEntity">Сущность</typeparam>
    public class RepositoryOperations<TEntity> : IRepositoryOperations<TEntity> where TEntity : class
    {
        protected ApplicationDbContext Context { get; set; }
        private DbSet<TEntity> DbSet;

        public RepositoryOperations(ApplicationDbContext context)
        {
            Context = context;
            DbSet = context.Set<TEntity>();
        }

        /// <summary>
        /// Полные данные объекта, включая данные других объектов через внешние связи
        /// Может быть переопределён в конкретной реализации объекта данных
        /// По умолчанию соответствует значению PlainData
        /// </summary>
        public virtual IQueryable<TEntity> Data => DbSet;

        /// <summary>
        /// Данные только одной таблицы.
        /// Объекты (поля) связей с другими таблицами не заполнены (null)
        /// </summary>
        public IQueryable<TEntity> PlainData => DbSet;

        /// <summary>
        /// Получить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param>
        /// <returns>Сущность</returns>
        public virtual async Task<TEntity?> GetAsync(int id)
            => await DbSet.FindAsync(id);

        /// <summary>
        /// Добавить один объект в набор данных
        /// </summary>
        /// <param name="entity">Добавляемый объект</param>
        /// <returns>Объект после добавления в БД</returns>
        public virtual async Task<TEntity> AddAsync(TEntity entity)
        {
            EntityEntry<TEntity> value = await DbSet.AddAsync(entity);
            ApplieAuditable();
            await Context.SaveChangesAsync();
            return value.Entity;
        }

        /// <summary>
        /// Добавить коллекцию объектов в набор данных
        /// </summary>
        /// <param name="entities">Добавляемые объекты</param>
        /// <returns>Объекты после добавления в БД</returns>
        public virtual async Task<IEnumerable<TEntity>> AddRangeAsync(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return [];
            await DbSet.AddRangeAsync(entityList);
            ApplieAuditable();
            await Context.SaveChangesAsync();
            return entityList;
        }

        /// <summary>
        /// Изменить один объект в наборе данных
        /// </summary>
        /// <param name="entity">Изменяемый объект</param>
        public virtual async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            ApplieAuditable();
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Изменить коллекцию объектов в наборе данных
        /// </summary>
        /// <param name="entities">Изменяемые объекты</param>
        public virtual async Task UpdateRange(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return;
            DbSet.UpdateRange(entityList);
            ApplieAuditable();
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="id">Id объекта</param>
        public virtual async Task Delete(int id)
        {
            var entity = await GetAsync(id);
            if (entity == null)
                return;
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="ids">Id объектов</param>
        public virtual async Task DeleteRange(IEnumerable<int> ids)
        {
            List<int> idList = [.. ids];
            if (idList.Count == 0)
                return;
            List<TEntity> entityList = await DbSet.Where(e => idList.Contains(EF.Property<int>(e, "Id")))
                    .ToListAsync();
            if (entityList.Count == 0)
                return;
            DbSet.RemoveRange(entityList);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить один объект из набора данных
        /// </summary>
        /// <param name="entity">Удаляемый объект</param>
        public virtual async Task Delete(TEntity entity)
        {
            DbSet.Remove(entity);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить коллекцию объектов из набора данных
        /// </summary>
        /// <param name="entities">Удаляемые объекты</param>
        public virtual async Task DeleteRange(IEnumerable<TEntity> entities)
        {
            List<TEntity> entityList = [.. entities];
            if (entityList.Count == 0)
                return;
            DbSet.RemoveRange(entityList);
            await Context.SaveChangesAsync();
        }

        /// <summary>
        /// Установка значений аудита
        /// </summary>
        private void ApplieAuditable()
        {
            var entities = Context.ChangeTracker.Entries<AuditableEntityBase>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entity in entities)
            {
                switch (entity.State)
                {
                    case EntityState.Modified:
                        entity.Entity.CreatedDate = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entity.Entity.CreatedDate = DateTime.UtcNow;
                        entity.Entity.ChangedDate = DateTime.UtcNow;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
