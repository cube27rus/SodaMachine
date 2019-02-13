using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SodaMachine.Domain.Base.Interfaces;

namespace SodaMachine.Domain.Base
{
    public abstract class EntityBaseRepository<TEntity, TEntityId> : IEntityRepository<TEntity, TEntityId>
        where TEntity : class, IEntity<TEntityId>, new()
        where TEntityId : struct
    {
        public ApplicationContext DbContext { get; }

        public EntityBaseRepository(ApplicationContext dbContext)
        {
            DbContext = dbContext;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>()
                .AsQueryable();
        }

        public virtual IQueryable<TEntity> GetAll(int take, int skip)
        {
            return DbContext.Set<TEntity>()
                .Skip(skip)
                .Take(take);
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync()
        {
            IQueryable<TEntity> result;

            result = DbContext.Set<TEntity>();

            await result.LoadAsync<TEntity>();

            return result;
        }

        public virtual async Task<IQueryable<TEntity>> GetAllAsync(int take, int skip)
        {
            IQueryable<TEntity> result;

            result = DbContext.Set<TEntity>()
                .Skip(skip)
                .Take(take).AsQueryable<TEntity>();

            await result.LoadAsync<TEntity>();

            return result;
        }

        public virtual IEnumerable<TEntity> GetAllSortBy(string sortBy, bool sortAsc = true, int? take = null, int? skip = null)
        {
            IEnumerable<TEntity> result;

            var query = DbContext.Set<TEntity>();
            if (take != null && skip != null)
            {
                query
                .Skip(skip.Value)
                .Take(take.Value);
            }

            System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

            if (sortAsc)
            {
                result = query
                    .OrderBy(x => prop.GetValue(x, null))
                    .AsEnumerable();
            }
            else
            {
                result = query
                    .OrderByDescending(x => prop.GetValue(x, null))
                    .AsEnumerable();
            }

            return result;
        }


        public virtual async Task<IQueryable<TEntity>> GetAllSortByAsync(string sortBy, bool sortAsc = true, int? take = null, int? skip = null)
        {
            IQueryable<TEntity> result;

            var query = DbContext.Set<TEntity>();
            System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

            if (sortAsc)
            {
                result = query
                    .OrderBy(x => prop.GetValue(x, null))
                    .AsQueryable();
            }
            else
            {
                result = query
                    .OrderByDescending(x => prop.GetValue(x, null))
                    .AsQueryable();
            }

            if (take != null && skip != null)
            {
                result = result
                .Skip(skip.Value)
                .Take(take.Value);
            }

            await result.LoadAsync<TEntity>();
            return result;
        }

        public virtual IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query
                    .Include(includeProperty);
            }
            return query
                .AsEnumerable();
        }

        public virtual IEnumerable<TEntity> GetAllIncluding(string sortBy = null, bool? sortAsc = null,
            int? take = null, int? skip = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();

            foreach (var includeProperty in includeProperties)
            {
                query = query
                    .Include(includeProperty);
            }

            if (take != null && skip != null)
            {
                query
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            if (!string.IsNullOrEmpty(sortBy) && sortAsc.HasValue)
            {
                System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

                if (sortAsc.Value)
                {
                    query
                        .OrderBy(x => prop.GetValue(x, null));
                }
                else
                {
                    query
                        .OrderByDescending(x => prop.GetValue(x, null));
                }
            }

            return query
                .AsEnumerable();
        }

        public virtual int Count()
        {
            return DbContext.Set<TEntity>()
                .Count();
        }

        public virtual async Task<int> CountAsync()
        {
            return await DbContext.Set<TEntity>()
                .CountAsync();
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>()
                .Count(predicate);
        }

        public virtual async Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>()
                .CountAsync(predicate);
        }

        public virtual TEntity GetSingle(TEntityId id)
        {
            return DbContext.Set<TEntity>()
                .FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual TEntity GetSingle(TEntityId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(TEntityId id, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(TEntityId id)
        {
            return await DbContext.Set<TEntity>()
                .FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>()
                .FirstOrDefault(predicate);
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate)
        {
            return await DbContext.Set<TEntity>()
                .FirstOrDefaultAsync(predicate);
        }

        public virtual TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate)
                .FirstOrDefault();
        }

        public virtual async Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return await query.Where(predicate)
                .FirstOrDefaultAsync();
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>()
                .Where(predicate);
        }

        public virtual IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            return query.Where(predicate);
        }

        public virtual IQueryable<TEntity> FindBy(int take, int skip, Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>()
                .Where(predicate)
                .Skip(skip)
                .Take(take);
        }

        public virtual IEnumerable<TEntity> FindBy(int take, int skip, Expression<Func<TEntity, bool>> predicate, string sortBy, bool sortAsc)
        {
            IEnumerable<TEntity> result;

            IQueryable<TEntity> query = DbContext.Set<TEntity>()
                .Where(predicate)
                .Skip(skip)
                .Take(take);

            System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

            if (sortAsc)
            {
                result = query
                    .OrderBy(x => prop.GetValue(x, null))
                    .AsEnumerable();
            }
            else
            {
                result = query
                    .OrderByDescending(x => prop.GetValue(x, null))
                    .AsEnumerable();
            }

            return result;
        }

        public virtual async Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, string sortBy = null, bool sortAsc = true, int? take = null, int? skip = null)
        {
            IQueryable<TEntity> result;

            IQueryable<TEntity> query = DbContext.Set<TEntity>()
                .Where(predicate);

            result = query.AsQueryable();
            if (!string.IsNullOrEmpty(sortBy))
            {
                System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

                if (sortAsc)
                {
                    result = query
                        .OrderBy(x => prop.GetValue(x, null))
                        .AsQueryable();
                }
                else
                {
                    result = query
                        .OrderByDescending(x => prop.GetValue(x, null))
                        .AsQueryable();
                }
            }

            if (take != null && skip != null)
            {
                result = result
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            await result.LoadAsync();

            return result;
        }

        public virtual async Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            string sortBy, bool sortAsc = true, int? take = null, int? skip = null,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>()
                .Where(predicate);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                System.Reflection.PropertyInfo prop = typeof(TEntity).GetProperty(sortBy);

                if (sortAsc)
                {
                    query = query
                        .OrderBy(x => prop.GetValue(x, null))
                        .AsQueryable();
                }
                else
                {
                    query = query
                        .OrderByDescending(x => prop.GetValue(x, null))
                        .AsQueryable();
                }
            }

            if (take != null && skip != null)
            {
                query = query
                    .Skip(skip.Value)
                    .Take(take.Value);
            }

            await query.LoadAsync();

            return query;
        }

        public virtual async Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate,
            params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = DbContext.Set<TEntity>()
                .Where(predicate);

            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }

            await query.LoadAsync();

            return query;
        }


        public virtual IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate)
        {
            return DbContext.Set<TEntity>()
                .AsNoTracking()
                .Where(predicate);
        }

        public virtual EntityEntry<TEntity> Add(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            return DbContext.Set<TEntity>()
                .Add(entity);
        }

        public virtual void AddorUpdate(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            var currEntity = this.GetSingle(entity.Id);
            if (currEntity == null)
            {
                this.Add(entity);
            }
            else
            {
                this.Update(entity);
            }
        }

        public virtual async Task<EntityEntry<TEntity>> AddAsync(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            return await DbContext.Set<TEntity>()
                .AddAsync(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            if (!IsEntityAlreadyTracked(entity))
            {
                EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
                DbContext.Set<TEntity>()
                    .Attach(entity);
            }
        }

        public virtual void Update(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Modified;
        }

        public virtual void Delete(TEntity entity)
        {
            EntityEntry dbEntityEntry = DbContext.Entry<TEntity>(entity);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public virtual void DeleteWhere(Func<TEntity, bool> predicate)
        {
            IEnumerable<TEntity> entities = DbContext.Set<TEntity>().Where(predicate);

            foreach (var entity in entities)
            {
                DbContext.Entry<TEntity>(entity).State = EntityState.Deleted;
            }
        }

        public virtual void SyncOfflineCollection<TOfflineItem>(ICollection<TOfflineItem> collection, TEntity parent)
        {
            foreach (TOfflineItem item in collection)
            {
                // TODO
            }
        }

        private bool IsEntityAlreadyTracked(TEntity entity)
        {
            var entityType = typeof(TEntity);
            var baseEntity = entity as IEntity<int>;
            if (baseEntity == null)
            {
                return false;
            }
            int baseEntityId = baseEntity.Id;

            return DbContext.ChangeTracker
                .Entries()
                .Any(w => w.Entity.GetType() == entityType &&
                     w.Entity.GetType().IsSubclassOf(typeof(EntityBase<>)) &&
                     (w.Entity as IEntity<int>).Id == baseEntityId);
        }
    }
}
