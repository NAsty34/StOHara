using Data.Model;
using Data.Model.Options;
using Data.Repository.Interface;
using Logic.Exceptions;
using Logic.Service.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Logic.Service;

public class FilesService : BaseService<FileEntity>, IFilesService
{
    private readonly IFilesRepository _fileRepository;
    private readonly IScopeInfo _scope;
    private readonly FilesOptions _filesOptions;

    public FilesService(IBaseRepository<FileEntity?> baseRepository, IScopeInfo scope, IFilesRepository fileRepository,
        IOptions<FilesOptions> filesOptions) : base(baseRepository)
    {
        _scope = scope;
        _fileRepository = fileRepository;
        _filesOptions = filesOptions.Value;
    }


    public string GetUrlFile(FileEntity file)
    {
        return $"{_filesOptions.RootPath}/{_filesOptions.PathFolder}/{file.Id}{file.Extension}";
    }

    public List<string> GetUrlFile(IEnumerable<Guid> idFile)
    {
        var urls = new List<string>();
        var files = _fileRepository.GetByIds(idFile);
        foreach (var oneFile in files)
        {
            urls.Add($"{_filesOptions.RootPath}/{_filesOptions.PathFolder}/{oneFile.Id}{oneFile.Extension}");
        }

        return urls;
    }

    public string GetUrlFile(Guid idFile)
    {
        var file = _fileRepository.GetById(idFile);
        if (file == null) return null;
        return $"{_filesOptions.RootPath}/{_filesOptions.PathFolder}/{idFile}{file.Extension}";
    }

    public async Task<FileEntity> AddFile(IFormFile file)
    {
        var path = $@"{_filesOptions.PathFolder}";
        var dirInfo = new DirectoryInfo(path);

        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }
        var extension = Path.GetExtension(file.FileName);
        if (!_filesOptions.Extension.Contains(extension)) throw new ExtensionForFilesException();
        var fi = new FileEntity
        {
            Id = Guid.NewGuid(),
            Name = file.FileName,
            Extension = extension,
            IdUser = _scope.Userid
        };
        await _fileRepository.Create(fi);
        var fullPath = $"{_filesOptions.PathFolder}/{fi.Id}{fi.Extension}";
        await using (var fileStream = new FileStream(fullPath, FileMode.Append))
        {
            await file.CopyToAsync(fileStream);
        }
        return fi;
    }

    public async Task<IEnumerable<FileEntity>> AddFiles(IEnumerable<IFormFile> files)
    {
        var listFiles = new List<FileEntity>();
        var path = $@"{_filesOptions.PathFolder}";
        var dirInfo = new DirectoryInfo(path);

        if (!dirInfo.Exists)
        {
            dirInfo.Create();
        }

        foreach (var file in files)
        {
            var extension = Path.GetExtension(file.FileName);
            if (!_filesOptions.Extension.Contains(extension)) throw new ExtensionForFilesException();
            var fi = new FileEntity
            {
                Id = Guid.NewGuid(),
                Name = file.FileName,
                Extension = extension,
                IdUser = _scope.Userid
            };
            await _fileRepository.Create(fi);
            listFiles.Add(fi);
            var fullPath = $"{_filesOptions.PathFolder}/{fi.Id}{fi.Extension}";
            await using (var fileStream = new FileStream(fullPath, FileMode.Append))
            {
                await file.CopyToAsync(fileStream);
            }
        }

        return listFiles;
    }


    public async Task DeleteFileById(Guid fileId)
    {
        var file = _fileRepository.GetById(fileId);
        var path = $"{_filesOptions.PathFolder}/{file.Id}{file.Extension}";
        var fileInf = new FileInfo(path);
        if (fileInf.Exists)
        {
            fileInf.Delete();
        }

        await _fileRepository.Delete(fileId);
    }

    public async Task DeleteFileByIds(IEnumerable<Guid> filesId)
    {
        var listFiles = _fileRepository.GetByIds(filesId);
        foreach (var file in listFiles)
        {
            var path = $"{_filesOptions.PathFolder}/{file.Id}{file.Extension}";
            var fileInf = new FileInfo(path);
            if (fileInf.Exists)
            {
                fileInf.Delete();
            }
        }

        await _fileRepository.Delete(filesId);
    }
}