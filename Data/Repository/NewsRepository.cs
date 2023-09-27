using Data.Model;
using Data.Repository.Interface;

namespace Data.Repository;

public class NewsRepository : BaseRepository<NewsEntity>, INewsRepository
{
    public NewsRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
    }

    public override async Task<PageModel<NewsEntity>> GetPage(int? page, int? size)
    {
        return await DbSet.OrderByDescending(a => a.CreatorDate).GetPage(page, size);
    }
}