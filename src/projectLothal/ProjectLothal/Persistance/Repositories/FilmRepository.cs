using Application.Services;
using Core.Persistance.Repositories;
using Domain.Entities;
using Persistance.Contexts;

namespace Persistance.Repositories
{
    public class FilmRepository : EfRepositoryBase<Film, Guid, BaseDbContext>, IFilmRepository
    {
        public FilmRepository(BaseDbContext context) : base(context)
        {
        }
    }
}
