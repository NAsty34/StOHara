using Data.Model;
using Data.Model.Entities;
using Microsoft.AspNetCore.Http;

namespace Logic.Service.Interface;

public interface IFilesService:IBaseService<FileEntity>
{
    public Task<FileEntity> AddFile(IFormFile file);
    public Task<IEnumerable<FileEntity>> AddFiles(IEnumerable<IFormFile> files);
    public string GetUrlFile(FileEntity file);
    public Task<List<string>> GetUrlFile(List<Guid> idFile);
    public string GetUrlFile(Guid idFile);
    public Task DeleteFileById(Guid fileId);
    public Task DeleteFileByIds(List<Guid> filesId);
}