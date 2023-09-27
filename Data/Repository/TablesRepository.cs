using Data.Model;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class TablesRepository : ITablesRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<TablesEntity> DbSet;

    public TablesRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = DbSet = Context.Set<TablesEntity>();
    }

    /*public async Task<List<TablesEntity>> GetTablseBar()
    {
        return await DbSet.Where(a => a.Hall.Contains("бар")).OrderBy(a=>a.Reserve.EstimatedStartTime).ToListAsync();
    }*/

    public async Task<List<TablesEntity>> GetTablseHall()
    {
        return await DbSet.Where(a => a.Hall.Contains("Стол")).OrderBy(a=>a.Reserve.EstimatedStartTime).ToListAsync();
    }
    public async Task<List<TablesEntity>> GetTablseLaunge()
    {
        return await DbSet.Where(a => a.Hall== "Лаунж").OrderBy(a=>a.Reserve.EstimatedStartTime).ToListAsync();
    }

    /*public async Task<List<TablesEntity>> GetTablseStreet()
    {
        return await DbSet.Where(a => a.Hall.Contains("Веранда")).OrderBy(a=>a.Reserve.EstimatedStartTime).ToListAsync();
    }*/
    

    public TablesEntity GetById(string id)
    {
        return DbSet.First(a => a.Id == Guid.Parse(id));
    }
    
    public List<TablesEntity> GetByAll()
    {
        return DbSet.Where(a => true).ToList();
    }
    
    public IEnumerable<TablesEntity> GetByIds(IEnumerable<string> ids)
    {
        return DbSet.Where(a => ids.Contains(a.Id.ToString())).ToList();
    }

    public async Task Create(List<TablesEntity> t)
    {
        await DbSet.AddRangeAsync(t);
        await Context.SaveChangesAsync();
    }

    public async Task Edit(List<TablesEntity> t)
    {
        foreach (var baseEntity in t)
        {
            Context.Entry(baseEntity).State = EntityState.Modified;
        }

        await Context.SaveChangesAsync();
    }

    public async Task Edit(TablesEntity t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task Delete()
    {
        var range = DbSet.Where(a => a.Id != null).ToList();
        DbSet.RemoveRange(range);
        await Context.SaveChangesAsync();
    }
}