using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class MenuService:BaseService<MenuEntity>, IMenuService
{
    private readonly IMenuRepository _menuRepository;
    public MenuService(IBaseRepository<MenuEntity?> baseRepository, IMenuRepository menuRepository) : base(baseRepository)
    {
        _menuRepository = menuRepository;
    }

    public async Task<PageModel<MenuEntity>> GetMainPage(int? page, int? size)
    {
        return await _menuRepository.GetMainPage(page, size);
    }
    
    public async Task<PageModel<MenuEntity>> GetLaunchPage(int? page, int? size)
    {
        return await _menuRepository.GetLaunchPage(page, size);
    }
    
    public int GetMaxPosition()
    {
        return _menuRepository.GetMaxPosition();
    }
}