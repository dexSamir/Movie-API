using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class ActorRepository : GenericRepository<Actor>, IActorRepository
{
    public ActorRepository(AppDbContext context) : base(context)
    {
    }
}

