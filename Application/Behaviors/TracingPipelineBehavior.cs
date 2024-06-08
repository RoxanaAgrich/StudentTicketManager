using MediatR;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace Application.Behaviors
{
    public  class TracingPipelineBehavior<TRequest,TResponse>(ILogger<TRequest> logger):
       IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<TRequest> _logger = logger;
        private readonly Stopwatch _timer;
    public async Task<TResponse> Handle(TRequest request , RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            _timer.Start();
            TResponse? response = await next();
            _timer.Stop();
            var elapsedMilliseconds = _timer.ElapsedMilliseconds;
            if (elapsedMilliseconds <= 5000) 
                return response;
            var requestName = typeof(TRequest).Name;
            _logger.LogWarning("Long Time Running - Request Detail : {Name} ({ElapsedMilliseconds} milliseconds}) {@Request}",
                requestName,elapsedMilliseconds, request);
            return response;
        }
    }
}
