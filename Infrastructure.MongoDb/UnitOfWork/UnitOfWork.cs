using Domain.Abtractions.Entities;
using Domain.Abtractions.Repositories;
using Domain.Abtractions.UnitOfWork;
using Infrastructure.MongoDb.Repositories;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace Infrastructure.MongoDb.UnitOfWork;

internal class UnitOfWork : IUnitOfWorkMongboDb
{
    private readonly IMongoClient _client;
    private readonly IMongoDatabase _database;
    private readonly IDictionary<Type, object> _repositories;
    private IClientSessionHandle _session;
    private bool _disposed;

    public UnitOfWork(IConfiguration configuration)
    {
        var connectionString = configuration["MongoDBSettings:ConnectionString"];
        var databaseName = configuration["MongoDBSettings:DatabaseName"];

        _client = new MongoClient(connectionString);
        _database = _client.GetDatabase(databaseName);
        _repositories = new Dictionary<Type, object>();
    }

    public IRepositoryMongoDbBase<TEntity, TKey> GetRepository<TEntity, TKey>() where TEntity : Entity<TKey>
    {
        if (_repositories.ContainsKey(typeof(TEntity)))
        {
            return (IRepositoryMongoDbBase<TEntity, TKey>)_repositories[typeof(TEntity)];
        }

        var repositoryInstance = new RepositoryBase<TEntity, TKey>(_database, typeof(TEntity).Name);
        _repositories.Add(typeof(TEntity), repositoryInstance);
        return repositoryInstance;
    }

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default)
    {
        if (_session == null)
        {
            throw new InvalidOperationException("Session has not been started.");
        }

        await _session.CommitTransactionAsync(cancellationToken);
        return 1;
    }

    public async Task AbortAsync(CancellationToken cancellationToken = default)
    {
        if (_session == null)
        {
            throw new InvalidOperationException("Session has not been started.");
        }

        await _session.AbortTransactionAsync(cancellationToken);
    }

    public async Task<IClientSessionHandle> StartSessionAsync(CancellationToken cancellationToken = default)
    {
        _session = await _client.StartSessionAsync(cancellationToken: cancellationToken);
        _session.StartTransaction();
        return _session;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _session?.Dispose();
            }
        }
        _disposed = true;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}