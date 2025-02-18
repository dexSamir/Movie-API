using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class RentalRepository : GenericRepository<Rental>, IRentalRepository
{
    public RentalRepository(AppDbContext context) : base(context)
    {
    }
}

