using System.Reflection;
using FluentValidation;
using Handling.Application.Configuration.Behavior;
using Handling.Application.Configuration.Validation;
using MediatR;
using MicroElements.Swashbuckle.FluentValidation.AspNetCore;
using Microsoft.Extensions.DependencyInjection;

namespace Handling.Application.ServiceRegistration;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        // Add all FluentValidator handlers
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

        // Add Mediator
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

        // Add pipeline behavior for Mediator

        // services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviorProblemDetail<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));

        services.AddFluentValidationRulesToSwagger();
        return services;
    }
}