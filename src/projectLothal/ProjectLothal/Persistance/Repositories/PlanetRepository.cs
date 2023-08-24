using Application.Services;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories
{
    public class PlanetRepository : EfRepositoryBase<Planet, Guid, BaseDbContext>, IPlanetRepository
    {
        public PlanetRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
