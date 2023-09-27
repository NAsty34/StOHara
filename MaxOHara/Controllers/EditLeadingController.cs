using Data.Model;
using Data.Model.Lending;
using Logic.Service.Interface;
using MaxOHara.Dto;
using MaxOHara.Dto.Lending;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class EditLeadingController : Controller
{
    private readonly IFilesService _filesService;
    private readonly ILendingService<AboutLendingEntity> _lendingAboutService;
    private readonly ILendingService<AtmosphereLendingEntity> _lendingAtmosphereService;
    private readonly ILendingService<BannerLendingEntity> _lendingBannerService;
    private readonly ISliderService _lendingSliderService;

    public EditLeadingController(IFilesService filesService, ILendingService<BannerLendingEntity> lendingBannerService,
        ILendingService<AtmosphereLendingEntity> lendingAtmosphereService,
        ILendingService<AboutLendingEntity> lendingAboutService, ISliderService lendingSliderService)
    {
        _filesService = filesService;
        _lendingBannerService = lendingBannerService;
        _lendingAtmosphereService = lendingAtmosphereService;
        _lendingAboutService = lendingAboutService;
        _lendingSliderService = lendingSliderService;
    }

    [Route("/api/v1/lending")]
    [HttpGet]
    public async Task<EditLendingDto> GetLending()
    {
        var page = await _lendingBannerService.GetPage();
        var editBannerDto = new BannerLendingDto(page.BannerLendingEntity)
        {
            UrlFile = _filesService.GetUrlFile(page.BannerLendingEntity.IdFile)
        };
        var editAboutDto = page.AboutLendingEntities.Select(a => new AboutLendingDto(a)
        {
            UrlFile = _filesService.GetUrlFile(a.IdFile)
        }).ToList();
        var editAtmosphereDto = page.AtmosphereLendingEntities.Select(a => new AtmosphereLendingDto(a)
        {
            UrlFile = _filesService.GetUrlFile(a.IdFile)
        }).ToList();

        return new EditLendingDto(editBannerDto, editAboutDto, editAtmosphereDto);
    }


    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/lending/upload")]
    [HttpPost]
    public async Task<ResponseDto<FilesDto>> UploadFile([FromForm] IFormFile file)
    {
        var files = await _filesService.AddFile(file);
        var filesDto = new FilesDto(files)
        {
            File = _filesService.GetUrlFile(files)
        };
        return new(filesDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/lending")]
    [HttpPost]
    public async Task<EditLendingDto> EditLending([FromBody] EditLendingDto editLendingDto)
    {
        var listAbout = new List<AboutLendingEntity>();
        var listAtmosphere = new List<AtmosphereLendingEntity>();

        var editBanner = await _lendingBannerService.GetById(editLendingDto.BannerDto.Id);
        editBanner.Header = editLendingDto.BannerDto.Header;
        editBanner.IdFile = editLendingDto.BannerDto.IdFile;
        await _lendingBannerService.Edit(editBanner);

        var editBannerDto = new BannerLendingDto(editBanner)
        {
            UrlFile = _filesService.GetUrlFile(editBanner.IdFile)
        };

        var listEntityAbout = _lendingAboutService.GetByIds(editLendingDto.AboutDto.Select(a => a.Id));

        foreach (var oneAbout in listEntityAbout)
        {
            var oneAboutDto = editLendingDto.AboutDto.First(a => a.Id == oneAbout.Id);
            oneAbout.Header = oneAboutDto.Header;
            oneAbout.Description = oneAboutDto.Description;
            oneAbout.IsLeftPosition = oneAboutDto.IsLeftPosition;
            oneAbout.IdFile = oneAboutDto.IdFile;
            listAbout.Add(oneAbout);
        }

        await _lendingAboutService.Edit(listAbout);

        var editAboutDto = listAbout.Select(a => new AboutLendingDto(a)
        {
            UrlFile = _filesService.GetUrlFile(a.IdFile)
        }).ToList();

        var listEntityAtmosphere = _lendingAtmosphereService.GetByIds(editLendingDto.AtmosphereDto.Select(a => a.Id));
        foreach (var oneAtmosphere in listEntityAtmosphere)
        {
            var oneAtmosphereDto = editLendingDto.AtmosphereDto.First(a => a.Id == oneAtmosphere.Id);
            oneAtmosphere.Header = oneAtmosphereDto.Header;
            oneAtmosphere.Description = oneAtmosphereDto.Description;
            oneAtmosphere.IsLeftPosition = oneAtmosphereDto.IsLeftPosition;
            oneAtmosphere.IdFile = oneAtmosphereDto.IdFile;
            listAtmosphere.Add(oneAtmosphere);
        }

        await _lendingAtmosphereService.Edit(listAtmosphere);

        var editAtmosphereDto = listAtmosphere.Select(a => new AtmosphereLendingDto(a)
        {
            UrlFile = _filesService.GetUrlFile(a.IdFile)
        }).ToList();

        return new EditLendingDto(editBannerDto, editAboutDto, editAtmosphereDto);
    }

    [Route("/api/v1/lending/slider")]
    [HttpGet]
    public async Task<List<SliderLendingDto>> GetSlider(int? page, int? size)
    {
        var pageSlider = await _lendingSliderService.GetSlider();
        var editSliderDto = pageSlider.Select(a => new SliderLendingDto(a)
        {
            UrlFile = _filesService.GetUrlFile(a.IdFile)
        }).ToList();

        return new(editSliderDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/lending/slider")]
    [HttpPost]
    public async Task<SliderLendingDto> CreateSlider([FromBody] SliderLendingDto sliderDto)
    {
        var newSlider = new SliderLendingEntity()
        {
            Id = Guid.NewGuid(),
            Header = sliderDto.Header,
            IdFile = sliderDto.IdFile,
        };
        await _lendingSliderService.Create(newSlider);

        var editSliderDto = new SliderLendingDto(newSlider)
        {
            UrlFile = _filesService.GetUrlFile(newSlider.IdFile)
        };
        return editSliderDto;
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/lending/slider/{sliderId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteSlider(Guid sliderId)
    {
        var slider = await _lendingSliderService.GetById(sliderId);
        if (slider != null)
        {
            await _filesService.DeleteFileById(slider.IdFile);
            await _lendingSliderService.Delete(sliderId);    
        }
        return Ok();
    }
}