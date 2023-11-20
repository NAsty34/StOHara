using Data.Model;
using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface IBaseService<T>
{
    public T GetById(Guid id);
    public Task<List<T?>> GetByIds(IEnumerable<Guid> id);
    public Task<PageModel<T?>> GetPage(int? page, int? size);
    public Task<List<T>> Create(List<T> t);
    public Task Edit(List<T> t);
    public Task Delete (Guid id);
}