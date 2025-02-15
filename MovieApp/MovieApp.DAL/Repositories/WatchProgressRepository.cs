using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class WatchProgressRepository : GenericRepository<WatchProgress>, IWatchProgressRepository
{
    public WatchProgressRepository(AppDbContext context) : base(context)
    {
    }
}

