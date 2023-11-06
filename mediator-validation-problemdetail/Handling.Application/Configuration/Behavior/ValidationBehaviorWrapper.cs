using System.Reflection;
using FluentValidation;
using Handling.Application.Configuration.Validation;
using Handling.Application.Extensions;
using Handling.Common.ResponseWrapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handling.Application.Configuration.Behavior;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<Behavior.ValidationBehavior<TRequest, TResponse>> logger)
    {
        _validators = validators;
        _logger = logger;
    }
    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    { 
        var context = new ValidationContext<TRequest>(request);

        // Can be changed to support multiple validators 
        var validator = _validators.FirstOrDefault();
        if (validator == null) return next(); 
        
        var validationResult = validator.Validate(context); 
       
        if (validationResult.Errors.Count <= 0) return next();
        
        var parsedErrors = validationResult.Errors.Select(f => new ErrorDetails
        {
            PropertyName = f.PropertyName,
            Reason = f.ErrorMessage,
            ValidationCode = f.ErrorCode
        }).ToList();

        var validationErrors = _logger.LogValidation(parsedErrors, nameof(TRequest));
        return Task.FromResult(GetValidatableResult(validationErrors));

    }

    private static TResponse GetValidatableResult(List<string> validationFailures)
    {
#pragma warning disable CS8603
#pragma warning disable CS8602
#pragma warning disable CS8600
        var type = typeof(Result<>);
        var responseType = typeof(List<string>);

        MethodInfo method;
        if (!typeof(TResponse).IsGenericType)
        {
            type = typeof(Result);
            method = type.GetMethod("Failed", BindingFlags.Public | BindingFlags.Static, new[] { responseType });
            return (TResponse)method.Invoke(null, new[] { validationFailures }); 
        }


        method = type
             .MakeGenericType(typeof(TResponse)
             .GetGenericArguments())
             .GetMethod("Failed", new[] { responseType });

        return (TResponse)method.Invoke(null, new[] { validationFailures });

#pragma warning restore CS8600
#pragma warning restore CS8602
#pragma warning restore CS8603
    }
}