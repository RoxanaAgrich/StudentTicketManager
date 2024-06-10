using Domain.Abtractions.DbContext;
using Domain.Abtractions.UnitOfWork;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Behaviors;
public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly IApplicationDbContext _context;
    private readonly IUnitOfWorkMongboDb _unitOfWork;
    public TransactionPipelineBehavior(IApplicationDbContext context, IUnitOfWorkMongboDb unitOfWork)
    {
        _context = context;
        _unitOfWork = unitOfWork;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!TransactionPipelineBehavior<TRequest, TResponse>.IsCommandV1())
            return await next();
        if (TransactionPipelineBehavior<TRequest, TResponse>.IsCommandV1())
        {
            var strategy = _context.Database.CreateExecutionStrategy();
            return await strategy.ExecuteAsync(async () =>
            {
                await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);
                {
                    TResponse? response = await next();
                    await _context.SaveChangesAsync(cancellationToken);
                    await transaction.CommitAsync();
                    return response;
                }
            });
        }
        using var mongoTransaction = await _unitOfWork.StartSessionAsync(cancellationToken);
        {
            TResponse? response = await next();
            var commit = await _unitOfWork.CommitAsync(cancellationToken);
            if(commit != 1)
            {
                await _unitOfWork.AbortAsync(cancellationToken);
            }
            return response;
        }
    }
    private static bool IsCommandV1()
        => typeof(TRequest).Namespace is not null && typeof(TRequest).Namespace.Contains("V1");
}