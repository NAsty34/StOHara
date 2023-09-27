using Data.Model;
using Logic.Service.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class MenuController : Controller
{
    private readonly IFilesService _filesService;
    private readonly IMenuService _menuService;

    public MenuController(IFilesService filesService, IMenuService menuService)
    {
        _filesService = filesService;
        _menuService = menuService;
    }

    [Route("/api/v1/menu/main")]
    [HttpGet]
    public async Task<ResponseDto<PageModel<MenuDto>>> GetMainMenu(int? page, int? size)
    {
        var menu = await _menuService.GetMainPage(page, size);
        var pageMenu = PageModel<MenuDto>.Create(menu, menu.Items!.Select(a =>
        {
            var menuDto = new MenuDto(a)
            {
                File = _filesService.GetUrlFile(a.IdFile).ToString()
            };
            return menuDto;
        }));
        return new(pageMenu);
    }

    [Route("/api/v1/menu/lunch")]
    [HttpGet]
    public async Task<ResponseDto<PageModel<MenuDto>>> GetLunchMenu(int? page, int? size)
    {
        var menu = await _menuService.GetLaunchPage(page, size);
        var pageMenu = PageModel<MenuDto>.Create(menu, menu.Items!.Select(a =>
        {
            var menuDto = new MenuDto(a)
            {
                File = _filesService.GetUrlFile(a.IdFile).ToString()
            };
            return menuDto;
        }));
        return new(pageMenu);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/menu/upload/{typeMenu}")]
    [HttpPost]
    public async Task<ResponseDto<IEnumerable<MenuDto>>> CreateMenu([FromForm] IEnumerable<IFormFile> file, string typeMenu)
    {
        var menuEntities = new List<MenuEntity>();
        var maxPos = _menuService.GetMaxPosition();
        var newFile = await _filesService.AddFiles(file);
        if (typeMenu == "lunch")
        {
            menuEntities = newFile.Select(a => new MenuEntity()
            {
                Id = Guid.NewGuid(),
                Position = maxPos++,
                IdFile = a.Id,
                BusinessLunches = true
            }).ToList();
        }
        if (typeMenu == "main")
        {
            menuEntities = newFile.Select(a => new MenuEntity()
            {
                Id = Guid.NewGuid(),
                Position = maxPos++,
                IdFile = a.Id,
                BusinessLunches = false
            }).ToList();
        }

        await _menuService.Create(menuEntities);
        
        var listMenuDto = menuEntities.Select(a => new MenuDto(a)
        {
            File = _filesService.GetUrlFile(a.IdFile)
        });

        return new(listMenuDto);
    }
    

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/menu")]
    [HttpPut]
    public async Task<ResponseDto<PageModel<MenuDto>>> EditPositionMenu([FromBody] List<EditPositionMenu> positionMenu,
        int? page, int? size)
    {
        var dictionary = positionMenu.ToDictionary(a => a.Id, a => a.Position);
        var listMenu = _menuService.GetByIds(dictionary.Keys);
        foreach (var menu in listMenu)
        {
            menu.Position = dictionary[menu.Id];
        }

        await _menuService.Edit(listMenu);

        var pageModel = await _menuService.GetPage(page, size);
        var pageMenu = PageModel<MenuDto>.Create(pageModel, pageModel.Items!.Select(a =>
        {
            var menuDto = new MenuDto(a);
            menuDto.File = _filesService.GetUrlFile(a.IdFile).ToString();
            return menuDto;
        }));
        return new(pageMenu);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/menu/{menuId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteMenu(Guid menuId)
    {
        var menu = _menuService.GetById(menuId);
        await _filesService.DeleteFileById(menu.IdFile);
        await _menuService.Delete(menuId);
        return Ok();
    }
}