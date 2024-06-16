using Domain.Abtractions.Entities;
using System.Linq.Expressions;

namespace Domain.Abtractions.Repositories;

public interface IRepositoryMongoDbBase<TEntity, TKey> where TEntity : Entity<TKey>
{
    Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default);

    Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default);

    IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null);

    void Add(TEntity entity);

    void Update(TKey id, TEntity entity);

    void Remove(TKey id);

    void RemoveMultiple(List<TEntity> entities);
}
