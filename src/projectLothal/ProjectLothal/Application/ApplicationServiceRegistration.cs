using Core.Application.Rules;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application
{
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddSubClassOfType(Assembly.GetExecutingAssembly(), typeof(BaseBusinessRules));
            services.AddMediatR(configuration =>
            {
                configuration.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            });
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
