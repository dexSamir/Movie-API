using MovieApp.Core.Entities;
using MovieApp.Core.Repositories;
using MovieApp.DAL.Context;

namespace MovieApp.DAL.Repositories;
public class LanguageRepository : GenericRepository<Language>, ILanguageRepository
{
    public LanguageRepository(AppDbContext context) : base(context)
    {
    }
}

