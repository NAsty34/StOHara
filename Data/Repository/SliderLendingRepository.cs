using Data.Model.Lending;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class SliderLendingRepository:LendingRepository<SliderLendingEntity>, ISliderLendingRepository
{
    public SliderLendingRepository(MaxOHaraContext context) : base(context)
    {
    }
    public async Task<List<SliderLendingEntity>> GetSlider()
    {
        return await Context.SliderLending.ToListAsync();
    }
    public async Task Delete(Guid id)
    {
        var entity = GetById(id);
        if (entity != null) DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
    public async Task Create(SliderLendingEntity t)
    {
        await DbSet.AddAsync(t);
        await Context.SaveChangesAsync();
    }
}