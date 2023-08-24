
using Core.Persistance.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public interface ICharacterRepository : IAsyncRepository<Character, Guid>
    {
    }
}
