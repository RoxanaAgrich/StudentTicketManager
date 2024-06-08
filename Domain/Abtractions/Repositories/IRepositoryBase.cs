using System.Linq;
using System.Linq.Expressions;

namespace Domain.Abtractions.Repositories
{
    public interface IRepositoryBase<TEntity, in TKey> 
        where TEntity : class
    {
        Task<TEntity> FindByIdAsync (TKey Id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties);
        void Add(TEntity entity);
        void Update(TEntity entity);
        void Remove (TEntity entity);
        void RemoveMultiple (List<TEntity> entities);
    }
}
