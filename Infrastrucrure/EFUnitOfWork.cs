using Domain.Abtractions.UnitOfWork;

namespace Infrastrucrure;

internal class EFUnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _dbContext;

    public EFUnitOfWork(ApplicationDbContext dbContext)
        => _dbContext = dbContext;

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await _dbContext.SaveChangesAsync();
    }

    async ValueTask IAsyncDisposable.DisposeAsync()
    => await _dbContext.DisposeAsync();
}
