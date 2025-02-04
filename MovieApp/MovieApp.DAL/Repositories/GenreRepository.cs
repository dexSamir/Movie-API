using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class GenreRepository : GenericRepository<Genre>, IGenreRepository
{
    public GenreRepository(AppDbContext context) : base(context)
    {
    }
}

