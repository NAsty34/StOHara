using Data.Model.Lending;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class LendingRepository<T>:ILendingRepository<T> where T : BaseLending
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<T> DbSet;

    public LendingRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = Context.Set<T>();
    }
    
    public T? GetById(Guid id)
    {
        return DbSet.FirstOrDefault(a => a.Id == id);
    }
    public IEnumerable<T?> GetByIds(IEnumerable<Guid> ids)
    {
        return DbSet.Where(a => ids.Contains(a.Id)).ToList();
    }

    public async Task<EditLendingEntity> GetPage()
    {
        var banner = await Context.BannerLending.FirstAsync();
        var about = await Context.AboutLending.OrderBy(a=>a.Id).ToListAsync();
        var atmosphere = await Context.AtmosphereLending.OrderBy(a=>a.Id).ToListAsync();
        var edit = new EditLendingEntity()
        {
            BannerLendingEntity = banner,
            AboutLendingEntities = about,
            AtmosphereLendingEntities = atmosphere
        };
        return edit;
    }

    public async Task Edit(T t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task Edit(IEnumerable<T> t)
    {
        foreach (var baseEntity in t)
        {
            Context.Entry(baseEntity).State = EntityState.Modified;
        }
        await Context.SaveChangesAsync();
    }
    
}