 
using FluentValidation.Results;
using Handling.Application.Configuration.Validation;
using Microsoft.Extensions.Logging;

namespace Handling.Application.Extensions;

public static class LoggerExtensions
{
    public static List<string> LogValidation(this ILogger logger, List<ErrorDetails> validationResult, string handlerName)
    {
        var validationErrors = validationResult
            .Select(error => $"{error.PropertyName}-{error.Reason}-{error.ValidationCode}")
            .ToList();
        
        logger.LogError("Validation error while executing {HandlerName}. Errors {Join}", handlerName, string.Join("||",validationErrors));

        return validationErrors;
    }
}