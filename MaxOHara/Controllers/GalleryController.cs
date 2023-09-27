using Data.Model;
using Logic.Service.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class GalleryController : Controller
{
    private readonly IFilesService _filesService;
    private readonly IGalleryService _galleryService;

    public GalleryController(IFilesService filesService, IGalleryService galleryService)
    {
        _filesService = filesService;
        _galleryService = galleryService;
    }

    [Route("/api/v1/gallery")]
    [HttpGet]
    public async Task<ResponseDto<PageModel<GalleryDto>>> GetGallery(int? page, int? size)
    {
        var gallery = await _galleryService.GetPage(page, size);
        var pageGallery = PageModel<GalleryDto>.Create(gallery, gallery.Items!.Select(a =>
        {
            var galDto = new GalleryDto(a)
            {
                File = _filesService.GetUrlFile(a.IdFile)
            };
            return galDto;
        }));
        return new(pageGallery);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/gallery")]
    [HttpPost]
    public async Task<ResponseDto<IEnumerable<GalleryDto>>> CreateGallery([FromForm] IEnumerable<IFormFile> file)
    {
        var newFiles = await _filesService.AddFiles(file);
        var listGallery = new List<GalleryEntity>();
        foreach (var oneFile in newFiles)
        {
            var newGallery = new GalleryEntity()
            {
                Id = Guid.NewGuid(),
                IdFile = oneFile.Id
            };
            listGallery.Add(newGallery);
        }
        await _galleryService.Create(listGallery);

        var galleryDto = listGallery.Select(a=>new GalleryDto(a)
        {
            File = _filesService.GetUrlFile(a.IdFile)
        });

        return new(galleryDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin) + "," + nameof(RoleEntity.Сотрудник))]
    [Route("/api/v1/gallery/{galleryId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteGallery(Guid galleryId)
    {
        var gallery = _galleryService.GetById(galleryId);
        await _filesService.DeleteFileById(gallery.IdFile);
        await _galleryService.Delete(gallery.Id);
        return Ok();
    }
}