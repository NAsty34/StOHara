using Data.Model;

namespace Data.Repository.Interface;

public interface IMenuRepository:IBaseRepository<MenuEntity>
{
    public int GetMaxPosition();
    public Task<PageModel<MenuEntity>> GetMainPage(int? page, int? size);
    public Task<PageModel<MenuEntity>> GetLaunchPage(int? page, int? size);
}