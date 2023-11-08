using System.Diagnostics;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Handling.Application.Configuration.Behavior;

public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
    private readonly HttpContext? _httpContext;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger, IHttpContextAccessor contextAccessor)
    {
        _logger = logger;
        _httpContext = contextAccessor.HttpContext;
    }


    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = request.GetType().Name;
        var traceId = Activity.Current?.Id ?? _httpContext?.TraceIdentifier; 
        var requestNameWithTraceId = $"{requestName} - Trace Id: [{traceId}]";

        _logger.LogInformation("[START] {RequestNameWithGuid}", requestNameWithTraceId);
        TResponse response;

        var stopwatch = Stopwatch.StartNew();
        try
        {
            try
            {
                _logger.LogInformation("[PROPS] {RequestNameWithGuid} {Serialize}", requestNameWithTraceId,
                    JsonSerializer.Serialize(request));
            }
            catch (NotSupportedException)
            {
                _logger.LogInformation("[Serialization ERROR] {RequestNameWithGuid} Could not serialize the request",
                    requestNameWithTraceId);
            }

            response = await next();
        }
        
        finally
        {
            stopwatch.Stop();
            _logger.LogInformation("[END] {RequestNameWithGuid}; Execution time={StopwatchElapsedMilliseconds}ms",
                requestNameWithTraceId, stopwatch.ElapsedMilliseconds);
        }

        return response;
    }
}