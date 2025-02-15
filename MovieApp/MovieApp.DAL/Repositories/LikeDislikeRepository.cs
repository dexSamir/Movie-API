using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class LikeDislikeRepository : GenericRepository<LikeDislike>, ILikeDislikeRepository
{
    public LikeDislikeRepository(AppDbContext context) : base(context)
    {
    }
}

