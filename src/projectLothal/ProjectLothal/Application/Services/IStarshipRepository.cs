
using Core.Persistance.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public interface IStarshipRepository : IAsyncRepository<Starship, Guid>
    {
    }
}
