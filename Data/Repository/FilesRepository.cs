using Data.Model.Entities;
using Data.Repository.Interface;

namespace Data.Repository;

public class FilesRepository: BaseRepository<FileEntity>, IFilesRepository
{
    public FilesRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
    }

}