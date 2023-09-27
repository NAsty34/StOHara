using Data.Model.Lending;

namespace Data.Repository.Interface;

public interface ISliderLendingRepository:ILendingRepository<SliderLendingEntity>
{
    public Task<List<SliderLendingEntity>> GetSlider();
    public Task Create(SliderLendingEntity t);
    public Task Delete(Guid id);
}