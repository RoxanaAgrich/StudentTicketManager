using Domain.Abtractions.Entities;
using Domain.Abtractions.Repositories;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Linq;
using System.Linq.Expressions;

namespace Infrastructure.MongoDb.Repositories;

internal class RepositoryBase<TEntity> : IRepositoryMongoDbBase<TEntity>
        where TEntity : Entity<string>
{
    private readonly IMongoCollection<TEntity> _collection;

    public RepositoryBase(IMongoDatabase database, string collectionName)
        => _collection = database.GetCollection<TEntity>(collectionName);

    public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null)
    {
        var query = _collection.AsQueryable();
        if (predicate != null)
        {
            query = query.Where(predicate);
        }

        return query;
    }

    public async Task<TEntity> FindByIdAsync(string id, CancellationToken cancellationToken = default)
    {
        var objectId = new ObjectId(id);
        var filter = Builders<TEntity>.Filter.Eq(doc => new ObjectId(doc.Id), objectId);
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

    public void Add(TEntity entity)
        => _collection.InsertOne(entity);

    public void Remove(string id)
    {
        var objectId = new ObjectId(id);
        _collection.DeleteOne(x => new ObjectId(x.Id) == objectId);
    }

    public void RemoveMultiple(List<TEntity> entities)
    {
        var ids = entities.Select(e => e.Id).ToList();
        var filter = Builders<TEntity>.Filter.In(e => e.Id, ids);
        _collection.DeleteMany(filter);
    }

    public void Update(string id, TEntity entity)
    {
        var objectId = new ObjectId(id);
        _collection.ReplaceOne(x => new ObjectId(x.Id) == objectId, entity);
    }
}