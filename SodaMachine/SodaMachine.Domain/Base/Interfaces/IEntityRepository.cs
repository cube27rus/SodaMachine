using System;
using System.Linq;
using System.Threading.Tasks;
using System.Linq.Expressions;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace SodaMachine.Domain.Base.Interfaces
{
    public interface IEntityRepository<TEntity, TEntityId> 
        where TEntity : class, IEntity<TEntityId>, new()
        where TEntityId : struct
    {
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> GetAll(int take, int skip);
        Task<IQueryable<TEntity>> GetAllAsync();
        Task<IQueryable<TEntity>> GetAllAsync(int take, int skip);
        IEnumerable<TEntity> GetAllSortBy(string sortBy, bool sortAsc = true, int? take = null, int? skip = null);
        Task<IQueryable<TEntity>> GetAllSortByAsync(string sortBy, bool sortAsc = true, int? take = null, int? skip = null);
        IEnumerable<TEntity> GetAllIncluding(params Expression<Func<TEntity, object>>[] includeProperties);
        IEnumerable<TEntity> GetAllIncluding(string sortBy = null, bool? sortAsc = null, int? take = null, int? skip = null, params Expression<Func<TEntity, object>>[] includeProperties);
        int Count();
        Task<int> CountAsync();
        int Count(Expression<Func<TEntity, bool>> predicate);
        Task<int> CountAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetSingle(TEntityId id);
        TEntity GetSingle(TEntityId id, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetFirstOrDefaultAsync(TEntityId id);
        Task<TEntity> GetFirstOrDefaultAsync(TEntityId id, params Expression<Func<TEntity, object>>[] includeProperties);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate);
        TEntity GetFirstOrDefault(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> GetFirstOrDefaultAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate);
        IQueryable<TEntity> FindBy(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, string sortBy = null, bool sortAsc = true, int? take = null, int? skip = null, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindBy(int take, int skip, Expression<Func<TEntity, bool>> predicate);
        IEnumerable<TEntity> FindBy(int take, int skip, Expression<Func<TEntity, bool>> predicate, string sortBy, bool sortAsc);
        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, string sortBy, bool sortAsc = true, int? take = null, int? skip = null);
        Task<IQueryable<TEntity>> FindByAsync(Expression<Func<TEntity, bool>> predicate, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindAsNoTracking(Expression<Func<TEntity, bool>> predicate);
        EntityEntry<TEntity> Add(TEntity entity);
        void AddorUpdate(TEntity entity);
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        void Attach(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        void DeleteWhere(Func<TEntity, bool> predicate);
    }
}
