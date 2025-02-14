using Microsoft.EntityFrameworkCore;
using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class RatingRepository : GenericRepository<Rating>, IRatingRepository
{
    
    public RatingRepository(AppDbContext context) : base(context)
    {

    }

}

