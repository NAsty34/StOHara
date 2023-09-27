using Data.Model;
using Microsoft.AspNetCore.Http;

namespace Logic.Service.Interface;

public interface IFilesService:IBaseService<FileEntity>
{
    public Task<FileEntity> AddFile(IFormFile file);
    public Task<IEnumerable<FileEntity>> AddFiles(IEnumerable<IFormFile> files);
    public string GetUrlFile(FileEntity file);
    public List<string> GetUrlFile(IEnumerable<Guid> idFile);
    public string GetUrlFile(Guid idFile);
    public Task DeleteFileById(Guid fileId);
    public Task DeleteFileByIds(IEnumerable<Guid> filesId);
}