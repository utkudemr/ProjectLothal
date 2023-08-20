using Application.Services;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories
{
    public class StarshipRepository : EfRepositoryBase<Starship, Guid, BaseDbContext>, IStarshipRepository
    {
        public StarshipRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
