using FluentValidation;
using Handling.Application.Configuration.Validation;
using Handling.Application.Extensions;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Handling.Application.Configuration.Behavior
{
    public class ValidationBehaviorProblemDetail<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : notnull, IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;
        private readonly ILogger<ValidationBehaviorProblemDetail<TRequest, TResponse>> _logger;

        public ValidationBehaviorProblemDetail(IEnumerable<IValidator<TRequest>> validators,
            ILogger<ValidationBehaviorProblemDetail<TRequest, TResponse>> logger)
        {
            _validators = validators;
            _logger = logger;
        }

        public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var errors = _validators
                .Select(v => v.Validate(request))
                .SelectMany(result => result.Errors)
                .Where(error => error != null)
                .ToList();

            // If no errors, continue
            if (!errors.Any()) return next();
            
            
            var parsedErrors = errors.Select(f => new ErrorDetails
            {
                PropertyName = f.PropertyName,
                Reason = f.ErrorMessage,
                ValidationCode = f.ErrorCode
            }).ToList();

            _logger.LogValidation(parsedErrors, request.GetType().Name);
            throw new InvalidCommandException("Validation failed", parsedErrors);

        }
    }
}