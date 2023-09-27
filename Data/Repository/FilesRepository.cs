using Data.Model;
using Data.Repository.Interface;

namespace Data.Repository;

public class FilesRepository: BaseRepository<FileEntity>, IFilesRepository
{
    public FilesRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
    }

}