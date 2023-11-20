using Data.Model.Entities;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class TablesRepository : ITablesRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<TableEntity> DbSet;

    public TablesRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = DbSet = Context.Set<TableEntity>();
    }
    
    public async Task<List<TableEntity>> GetTableHall()
    {
        return await DbSet.Where(a => a.Hall.Contains("Зал")).ToListAsync();
            /*.OrderBy(a=>a.Reserves!.Select(reserveEntity=>reserveEntity.EstimatedStartTime)).ToListAsync();*/
    }
    public async Task<List<TableEntity>> GetTableLounge()
    {
        return await DbSet.Where(a => a.Hall == "Лаунж").ToListAsync();
            /*.OrderBy(a=>a.Reserves!.Select(reserveEntity=>reserveEntity.EstimatedStartTime))*/
    }
    
    public TableEntity GetById(string id)
    {
        return DbSet.First(a => a.Id == Guid.Parse(id));
    }
    
    public IEnumerable<TableEntity> GetByAll()
    {
        return DbSet.Where(a => true).ToList();
    }
    
    public List<TableEntity> GetByIds(List<string> ids)
    {
        return DbSet.Where(a => ids.Contains(a.Id.ToString())).ToList();
    }

    public async Task Create(List<TableEntity> t)
    {
        await DbSet.AddRangeAsync(t);
        await Context.SaveChangesAsync();
    }
    public async Task Create(TableEntity t)
    {
        await DbSet.AddAsync(t);
        await Context.SaveChangesAsync();
    }

    public async Task Edit(List<TableEntity> t)
    {
        foreach (var baseEntity in t)
        {
            Context.Entry(baseEntity).State = EntityState.Modified;
        }

        await Context.SaveChangesAsync();
    }

    public async Task Edit(TableEntity t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task Delete()
    {
        var range = DbSet.Where(a => true).ToList();
        DbSet.RemoveRange(range);
        await Context.SaveChangesAsync();
    }
}