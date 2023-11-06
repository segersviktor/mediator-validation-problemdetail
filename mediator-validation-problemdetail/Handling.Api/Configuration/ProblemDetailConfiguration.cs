using Handling.Api.Response;
using Handling.Application.Configuration.Validation;
using Handling.Common;
using Microsoft.AspNetCore.Mvc;
using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;

namespace Handling.Api.Configuration;

public static class ProblemDetailConfiguration
{
    public static Action<ProblemDetailsOptions>? ConfigureProblemDetail(WebApplicationBuilder webApplicationBuilder)
    {
        return options =>
        { 
            // Only display detailed messages when in development mode    
            options.IncludeExceptionDetails = (ctx, ex) => false;

        
            // Map custom exceptions to the 404 Not Found status code and return custom problem details. 
            options.Map<AgeToYoungException>(ex => new ProblemDetails
            {
                Title = "Throw my custom exception logic - age to young",
                Status = StatusCodes.Status404NotFound,
                Detail = ex.Message,
            }); 
        
            options.Map<InvalidCommandException>(ex => new InvalidCommandProblemDetails(ex));

       
            // Map NotImplementedException to the 501 Not Implemented status code.
            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
        
            // You can configure the middleware to ignore any exceptions of the specified type.
            // This is useful if you have upstream middleware that  needs to do additional handling of exceptions.
            // Note that unlike Rethrow, additional information will not be added to the exception.
            options.Ignore<DivideByZeroException>();

            // Because exceptions are handled polymorphic, this will act as a "catch all" mapping, which is why it's added last.
            // If an exception other than NotImplementedException and HttpRequestException is thrown, this will handle it.
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError); 
        };
    }
}