using System.Reflection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using PortfolioEAI.Application.Common.Behaviors;

namespace PortfolioEAI.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        // Enregistrement de MediatR
        services.AddMediatR(cfg => 
        {
            // Enregistrement de tous les handlers dans l'assembly courant
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            
            // Ajout du comportement de validation
            cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));
        });
        // Enregistrement de tous les validators FluentValidation
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
