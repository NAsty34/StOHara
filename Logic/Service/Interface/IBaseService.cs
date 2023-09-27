using Data.Model;

namespace Logic.Service.Interface;

public interface IBaseService<T>
{
    public T? GetById(Guid id);
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> id);
    public Task<PageModel<T?>> GetPage(int? page, int? size);
    public Task<T?> Create(T? t);
    public Task<List<T?>> Create(List<T?> t);
    public Task<T?> Edit(T? t);
    public Task Edit(IEnumerable<T?> t);
    public Task Delete (Guid id);
}