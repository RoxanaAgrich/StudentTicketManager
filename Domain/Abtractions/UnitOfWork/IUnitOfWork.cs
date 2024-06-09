namespace Domain.Abtractions.UnitOfWork
{
    public interface IUnitOfWork : IAsyncDisposable
    {
        Task SaveChangesAsync(CancellationToken cancellationToken = default);
    }

}
