using System.Linq.Expressions;

namespace Domain.Abtractions.Repositories
{
    public interface IRepositoryMongoDbBase<TEntity> where TEntity : class
    {
        Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default);

        Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

        IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null);

        void Add(TEntity entity);

        void Update(string id, TEntity entity);

        void Remove(string id);

        void RemoveMultiple(List<TEntity> entities);
    }
}
