using Data.Model;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class BaseService<T> : IBaseService<T>
{
    protected readonly IBaseRepository<T?> BaseRepository;

    public BaseService(IBaseRepository<T?> baseRepository)
    {
        BaseRepository = baseRepository;
    }
    

    public T? GetById(Guid id)
    {
        var tId = BaseRepository.GetById(id);
        return tId;
    }
    
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> id)
    {
        return BaseRepository.GetByIds(id);
    }

    public virtual async Task<PageModel<T?>> GetPage(int? page, int? size)
    {
        return await BaseRepository.GetPage(page, size);
    }

    public async Task<List<T?>> Create(List<T?> t)
    {
        foreach (var oneEntity in t)
        {
            await BaseRepository.Create(oneEntity);
        }

        return t;
    }

    public virtual async Task<T?> Create(T? t)
    {
        await BaseRepository.Create(t);
        return t;
    }

    public virtual async Task<T?> Edit(T? t)
    {
        await BaseRepository.Edit(t);
        return t;
    }
    public async Task Edit(IEnumerable<T?> t)
    {
        await BaseRepository.Edit(t);
    }

    public async Task Delete(Guid id)
    {
        await BaseRepository.Delete(id);
    }
}