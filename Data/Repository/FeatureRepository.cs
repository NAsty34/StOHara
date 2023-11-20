using Data.Model.Entities;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class FeatureRepository:IFeatureRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<FeatureEntity> DbSet;

    public FeatureRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = Context.Set<FeatureEntity>();
    }

    public FeatureEntity GetBool()
    {
        return DbSet.First(a=>true);
    }
    public async Task Edit(bool check)
    {
        var fe = DbSet.First();
        fe.IsCheck = check;
        Context.Entry(fe).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }
}