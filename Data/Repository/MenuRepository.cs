using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;

namespace Data.Repository;

public class MenuRepository:BaseRepository<MenuEntity>, IMenuRepository
{
    public MenuRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
        
    }
    
    public async Task<PageModel<MenuEntity>> GetMainPage(int? page, int? size)
    {
        return await DbSet.Where(a=>!a.BusinessLunches).OrderByDescending(a=>a.Position).GetPage(page, size);
    }
    
    public async Task<PageModel<MenuEntity>> GetLaunchPage(int? page, int? size)
    {
        return await DbSet.Where(a=>a.BusinessLunches).OrderByDescending(a=>a.Position).GetPage(page, size);
    }
    
    public int GetMaxPosition()
    {
        if (DbSet.FirstOrDefault() == null) return 0;
        var list = DbSet.Max(a=>a.Position)+1;
        return list;
    }
}