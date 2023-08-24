
using Core.Persistance.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public interface IPlanetRepository : IAsyncRepository<Planet, Guid>
    {
    }
}
