using Domain.Abtractions.Repositories;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace Infrastrucrure.Repositoties
{
    public class RepositoryBase<TEntity,  TKey>: IRepositoryBase<TEntity, TKey>, IDisposable
        where TEntity : TEntity<TKey>
    {
        private readonly ApplicationDbContext _context;
        public RepositoryBase(ApplicationDbContext context)
        => _context = context;

       
        public void Dispose()
          => _context?.Dispose();
        public IQueryable<TEntity> FindAll(Expression<Func<TEntity, bool>>? predicate = null,
        params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> items = _context.Set<TEntity>().AsNoTracking();
            if (includeProperties != null)
                foreach (var includeProperty in includeProperties)
                    items = items.Include(includeProperty);

            if (predicate is not null)
                items = items.Where(predicate);

            return items;
        }

        public async Task<TEntity> FindByIdAsync(TKey Id, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null, includeProperties).AsTracking().SingleOrDefaultAsync(x => x.Id.Equals(Id), cancellationToken);

        public async Task<TEntity> FindSingleAsync(Expression<Func<TEntity, bool>>? predicate = null, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object>>[] includeProperties)
        => await FindAll(null,includeProperties).AsTracking().SingleOrDefaultAsync(predicate, cancellationToken);
        public void Add(TEntity entity)
        => _context.Add(entity);

        public async Task Remove(TEntity entity)
        => _context.Set<TEntity>().Remove(entity);

        public async Task RemoveMultiple(List<TEntity> entities)
        => await _context.Set<TEntity>().RemoveRange(entities);

        public async Task Update(TEntity entity)
        => await _context.Set<TEntity>().Update;
    }
}
