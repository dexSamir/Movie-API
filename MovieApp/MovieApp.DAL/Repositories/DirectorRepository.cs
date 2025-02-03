
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class DirectorRepository : GenericRepository<Director>, IDirectorRepository
{
    public DirectorRepository(AppDbContext context) : base(context)
    {
    }
}

