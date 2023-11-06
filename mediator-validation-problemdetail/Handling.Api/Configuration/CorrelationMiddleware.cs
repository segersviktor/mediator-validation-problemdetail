using Microsoft.Extensions.Primitives;

namespace Handling.Api.Configuration
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string CorrelationIdHeader = "X-Correlation-Id";

        public CorrelationIdMiddleware(RequestDelegate next) { 
            _next = next; 
        }

        public async Task Invoke(HttpContext context)
        {
            var correlationId = Guid.NewGuid();
            AddCorrelationIdHeaderToResponse(context, correlationId.ToString()); 
            await _next(context);
        }
 

        private static void AddCorrelationIdHeaderToResponse(HttpContext context, StringValues correlationId)
        { 
            context.Response.OnStarting(() =>
            {
                context.Response.Headers.Add(CorrelationIdHeader, new[] {correlationId.ToString()});
                return Task.CompletedTask;
            });
        }
    }    
}