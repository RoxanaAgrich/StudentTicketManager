using Domain.Abtractions.Entities;
using Domain.Abtractions.Repositories;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Infrastructure.MongoDb.Repositories;

internal class RepositoryBase<TEntity, TKey> : IRepositoryMongoDbBase<TEntity, TKey> where TEntity : Entity<TKey>
{
    private readonly IMongoCollection<TEntity> _collection;

    public RepositoryBase(IMongoDatabase database, string collectionName)
    {
        _collection = database.GetCollection<TEntity>(collectionName);
    }

    public async Task<TEntity> FindByIdAsync(TKey id, CancellationToken cancellationToken = default)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default)
    {
        if (predicate == null)
        {
            return await _collection.Find(_ => true).FirstOrDefaultAsync(cancellationToken);
        }

        return await _collection.Find(predicate).FirstOrDefaultAsync(cancellationToken);
    }

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null)
    {
        if (predicate == null)
        {
            return _collection.AsQueryable();
        }

        return _collection.AsQueryable().Where(predicate);
    }

    public void Add(TEntity entity)
    {
        _collection.InsertOne(entity);
    }

    public void Update(TKey id, TEntity entity)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        _collection.ReplaceOne(filter, entity);
    }

    public void Remove(TKey id)
    {
        var filter = Builders<TEntity>.Filter.Eq(e => e.Id, id);
        _collection.DeleteOne(filter);
    }

    public void RemoveMultiple(List<TEntity> entities)
    {
        var ids = entities.Select(e => e.Id).ToList();
        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        _collection.DeleteMany(filter);
    }
}
