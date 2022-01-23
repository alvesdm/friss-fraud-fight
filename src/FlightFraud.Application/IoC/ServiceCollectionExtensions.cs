using System.Reflection;
using FlightFraud.Application.Common.Behaviours;
using FlightFraud.Application.Fraud.Commands;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FlightFraud.Application.IoC
{

    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            //services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnhandledExceptionBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(AuthorizationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
            //services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

            services.AddTransient<IMatchingRulesService, MatchingRulesService>();

            return services;
        }
    }
}