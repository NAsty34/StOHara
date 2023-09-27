using Data.Model;

namespace Data.Repository.Interface;

public interface IBaseRepository<T>
{
    public T GetById(Guid id);
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> ids);
    public Task<PageModel<T>> GetPage(int? page, int? size);
    public Task Create(T t);
    public Task Edit(T t);
    public Task Edit(IEnumerable<T> t);
    public Task Delete(Guid id);
    public Task Delete(IEnumerable<Guid> id);
}