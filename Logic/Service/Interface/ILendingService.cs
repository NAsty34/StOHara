using Data.Model.Lending;

namespace Logic.Service.Interface;

public interface ILendingService<T>
{
    public Task<T> GetById(Guid id);
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> id);
    public Task<EditLendingEntity> GetPage();
    public Task<T?> Edit(T? t);
    public Task<List<T?>> Edit(List<T?> t);
}