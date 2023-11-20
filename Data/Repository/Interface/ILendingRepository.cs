using Data.Model.Lending;

namespace Data.Repository.Interface;

public interface ILendingRepository<T>
{
    public T? GetById(Guid id);
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> ids);
    public Task<EditLendingEntity>GetPage();
    public Task Edit(T t);
    public Task Edit(IEnumerable<T> t);
}