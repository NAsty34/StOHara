using Data.Model.Lending;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class SliderLendingService:LendingService<SliderLendingEntity>, ISliderService
{
    private readonly ISliderLendingRepository _lendingRepository;
    public SliderLendingService(ISliderLendingRepository lendingRepository) : base(lendingRepository)
    {
        _lendingRepository = lendingRepository;
    }
    public async Task<List<SliderLendingEntity>> GetSlider()
    {
        return await _lendingRepository.GetSlider();
    }
    public async Task<SliderLendingEntity> Create(SliderLendingEntity? t)
    {
        await _lendingRepository.Create(t);
        return t;
    }
    public async Task Delete(Guid id)
    {
        await _lendingRepository.Delete(id);
    }
}