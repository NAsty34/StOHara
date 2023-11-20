using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;

namespace Data.Repository;

public class GalleryRepository:BaseRepository<GalleryEntity>, IGalleryRepository
{
    public GalleryRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
    }
    
    public override async Task<PageModel<GalleryEntity>> GetPage(int? page, int? size)
    {
        return await DbSet.OrderByDescending(a=>a.CreatorDate).GetPage(page, size);
    }
    
}