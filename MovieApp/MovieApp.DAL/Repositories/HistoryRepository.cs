using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class HistoryRepository : GenericRepository<History>, IHistoryRepository
{
    public HistoryRepository(AppDbContext context) : base(context)
    {
    }
}

