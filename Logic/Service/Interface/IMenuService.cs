using Data.Model;
using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface IMenuService:IBaseService<MenuEntity>
{
    public Task<PageModel<MenuEntity>> GetMainPage(int? page, int? size);
    public Task<PageModel<MenuEntity>> GetLaunchPage(int? page, int? size);
    public int GetMaxPosition();
}