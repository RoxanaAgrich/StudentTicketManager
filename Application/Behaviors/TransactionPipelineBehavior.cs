
using Infrastrucrure.Repositoties;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Behaviors;
public class TransactionPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
{
    private readonly ApplicationDbContext _context;
    public TransactionPipelineBehavior(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!TransactionPipelineBehavior<TRequest, TResponse>.IsCommand())
            return await next();
        #region ============== SQL-SERVER-STRATEGY-1 ==============
        var strategy = _context.Database.CreateExecutionStrategy();
        return await strategy.ExecuteAsync(async () =>
        {
            await using var transaction = await _context.Database.BeginTransactionAsync();
            {
                TResponse? response = await next();
                await _context.SaveChangesAsync();
                await transaction.CommitAsync();
                return response;
            }
        });
    }
    private static bool IsCommand()
        => typeof(TRequest).Name.EndsWith("Command");
}