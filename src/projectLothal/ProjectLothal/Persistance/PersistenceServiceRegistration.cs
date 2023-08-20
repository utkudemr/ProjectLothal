using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistance.Contexts;
using Persistance.Repositories;

namespace Persistance
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistanceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<BaseDbContext>(options => options.UseInMemoryDatabase("projectLothal"));
            services.AddScoped<IStarshipRepository, StarshipRepository>();
            return services;
        }
    }
}
