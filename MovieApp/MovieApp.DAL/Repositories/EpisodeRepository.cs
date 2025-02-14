using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class EpisodeRepository : GenericRepository<Episode>, IEpisodeRepository
{
    public EpisodeRepository(AppDbContext context) : base(context)
    {
    }
}

