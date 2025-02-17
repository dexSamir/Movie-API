using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class RentalService : GenericRepository<Rental>, IRentalRepository
{
    public RentalService(AppDbContext context) : base(context)
    {
    }
}

