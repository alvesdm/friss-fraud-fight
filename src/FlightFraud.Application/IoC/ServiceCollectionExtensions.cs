using System.Reflection;
using FightFraud.Application.Common.Behaviours;
using FightFraud.Application.Common.Interfaces;
using FightFraud.Application.Fraud.Services;
using FightFraud.Application.Fraud.Services.NameMatchingCalculatorImplementations;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FightFraud.Application.IoC
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            services.AddTransient<IMatchingCalculatorService, MatchingCalculatorService>();
            services.AddTransient<IMatchingRuleSettingsService, MatchingRuleSettingsService>();
            services.AddTransient<INameMatchingCalculator, LevenshteinDistanceNameMatchingCalculator>();

            return services;
        }
    }
}