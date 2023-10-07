using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using FluentValidation;
using Core.Application.Pipelines.Validation;
using Core.Application.Pipelines.Transaction;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Logging;
using Core.CrossCuttingConcerns.SeriLog;
using Core.CrossCuttingConcerns.SeriLog.Logger;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSubClassOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                configuration.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
                configuration.AddOpenBehavior(typeof(TransactionRequestBehavior<,>));
                configuration.AddOpenBehavior(typeof(CachingBehavior<,>));
                configuration.AddOpenBehavior(typeof(CacheRemovingBehavior<,>));
                configuration.AddOpenBehavior(typeof(LoggingBehavior<,>));
            });

            //services.AddSingleton<BaseLoggerService, FileLogger>();
            services.AddSingleton<BaseLoggerService, MsSqlLogger>();
            return services;
        }

        public static IServiceCollection AddSubClassOfType(this IServiceCollection services,Assembly assembly,Type type, Func<IServiceCollection, Type ,IServiceCollection>? addWithLifeCycle=null) {
            
            var types=assembly.GetTypes().Where(a=>a.IsSubclassOf(type) && type!=a).ToList();
            foreach(var item in types)
            {
                if (addWithLifeCycle == null)
                    services.AddScoped(item);
                else addWithLifeCycle(services, type);
                 
            }
            return services;
        }
    }
}
