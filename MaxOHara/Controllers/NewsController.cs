using Data.Model;
using Logic.Exceptions;
using Logic.Service.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class NewsController : Controller
{
    /*private readonly INewsService _newsService;
    private readonly IFilesService _filesService;

    public NewsController(INewsService newsService, IFilesService filesService)
    {
        _newsService = newsService;
        _filesService = filesService;
    }

    [Route("/api/v1/news")]
    [HttpGet]
    public async Task<ResponseDto<PageModel<NewsDto>>> GetNews(int? page, int? size)
    {
        var news = await _newsService.GetPage(page, size);
        var pageNews = PageModel<NewsDto>.Create(news, news.Items!.Select(a =>
        {
            var newsDto = new NewsDto(a);
            if (a.IdFile != null)
            {
                newsDto.File = _filesService.GetUrlFile(a.IdFile);
            }

            return newsDto;
        }));
        return new(pageNews);
    }

    [Route("/api/v1/news/{newsId}")]
    [HttpGet]
    public async Task<ResponseDto<NewsDto>> GetOneNews(Guid newsId)
    {
        var news = _newsService.GetById(newsId);
        var dtoNews = new NewsDto(news);
        if (news.IdFile != null)
        {
            dtoNews.File = _filesService.GetUrlFile(news.IdFile);
        }
        return new(dtoNews);
    }
    
    [Authorize(Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/news")]
    [HttpPost]
    public async Task<ResponseDto<NewsDto>> CreateNews([FromBody] CreateNewsDto newsDto)
    {
        var newNews = new NewsEntity
        {
            Id = Guid.NewGuid(),
            Header = newsDto.Header,
            Description = newsDto.Description,
            IdFile = newsDto.IdFile
        };
        await _newsService.Create(newNews);
        var newNewsDto = new NewsDto(newNews)
        {
            File = _filesService.GetUrlFile(newNews.IdFile)
        };
        return new(newNewsDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/news/upload")]
    [HttpPost]
    public async Task<ResponseDto<List<FilesDto>>> UploadFile([FromForm] IEnumerable<IFormFile> file)
    {
        var files = await _filesService.AddFiles(file);

        var filesDto = files.Select(a => new FilesDto(a)
        {
            File = _filesService.GetUrlFile(a)
        }).ToList();

        return new(filesDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/news/{newsId}")]
    [HttpPut]
    public async Task<ResponseDto<NewsDto>> EditNews([FromBody] CreateNewsDto newsDto, Guid newsId)
    {
        var fromDb = _newsService.GetById(newsId);
        if (fromDb == null) throw new NewsNotFoundException();
        fromDb.Header = newsDto.Header;
        fromDb.Description = newsDto.Description;
        fromDb.IdFile = newsDto.IdFile;
        var editNews = await _newsService.Edit(fromDb);
        
        var newsFromDbDto = new NewsDto(editNews);
        if (editNews.IdFile != null)
        {
            newsFromDbDto.File = _filesService.GetUrlFile(editNews.IdFile);
        }

        return new(newsFromDbDto);
    }

    [Authorize(Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/news/{newsId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteNews(Guid newsId)
    {
        var news = _newsService.GetById(newsId);
        if (news.IdFile != null)
        {
            await _filesService.DeleteFileByIds(news.IdFile);   
        }
        await _newsService.Delete(newsId);
        return Ok();
    }*/
}