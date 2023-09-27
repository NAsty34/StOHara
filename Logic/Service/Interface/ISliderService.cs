using Data.Model.Lending;

namespace Logic.Service.Interface;

public interface ISliderService:ILendingService<SliderLendingEntity>
{
    public Task<List<SliderLendingEntity>> GetSlider();
    public Task<SliderLendingEntity> Create(SliderLendingEntity? t);
    public Task Delete(Guid id);
}