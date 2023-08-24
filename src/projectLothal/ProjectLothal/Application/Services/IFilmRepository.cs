
using Core.Persistance.Repositories;
using Domain.Entities;

namespace Application.Services
{
    public interface IFilmRepository : IAsyncRepository<Film, Guid>
    {
    }
}
