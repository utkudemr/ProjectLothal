using Application.Services;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories
{
    public class CharacterRepository : EfRepositoryBase<Character, Guid, BaseDbContext>, ICharacterRepository
    {
        public CharacterRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
