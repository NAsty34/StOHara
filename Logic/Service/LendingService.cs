using Data.Model.Lending;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class LendingService<T>:ILendingService<T>
{
    protected readonly ILendingRepository<T> LendingRepository;

    public LendingService(ILendingRepository<T> lendingRepository)
    {
        LendingRepository = lendingRepository;
    }
    
    public async Task<T> GetById(Guid id)
    {
        var tId = LendingRepository.GetById(id);
        return tId;
    }
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> id)
    {
        return LendingRepository.GetByIds(id);
    }
    public async Task<EditLendingEntity> GetPage()
    {
        return await LendingRepository.GetPage();
    }

    public async Task<T?> Edit(T? t)
    {
        await LendingRepository.Edit(t);
        return t;
    }

    public async Task<List<T?>> Edit(List<T?> t)
    {
        await LendingRepository.Edit(t);
        return t;
    }
    
}