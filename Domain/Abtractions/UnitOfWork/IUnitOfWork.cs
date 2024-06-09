namespace Domain.Abtractions.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        Task SaveChangeAsync (CancellationToken cancellationToken = default);
    }
}
