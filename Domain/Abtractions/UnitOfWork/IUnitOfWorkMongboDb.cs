using Domain.Abtractions.Entities;
using Domain.Abtractions.Repositories;
using MongoDB.Driver;

namespace Domain.Abtractions.UnitOfWork
{
    public interface IUnitOfWorkMongboDb : IDisposable
    {
        Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken = default);
        IRepositoryMongoDbBase<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>;
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
        Task AbortAsync(CancellationToken cancellationToken = default);
    }
}
