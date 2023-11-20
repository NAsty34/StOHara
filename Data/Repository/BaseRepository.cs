using Data.Model;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class BaseRepository<T>: IBaseRepository<T> where T : BaseEntity
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<T> DbSet;
    private readonly IScopeInfo _scope;

    public BaseRepository(MaxOHaraContext context, IScopeInfo scope)
    {
        Context = context;
        _scope = scope;
        DbSet = Context.Set<T>();
    }
    

    public T GetById(Guid id)
    {
        return DbSet.First(a => a.Id == id);
    }

    public async Task<List<T>> GetByIds(IEnumerable<Guid> ids)
    {
        return await DbSet.Where(a => ids.Contains(a.Id)).ToListAsync();
    }

    public virtual async Task<PageModel<T>> GetPage(int? page, int? size)
    {
        return await DbSet.GetPage(page, size);
    }
    
    public async Task Create(T t)
    {
        t.IdUser = _scope.Userid;  
        t.CreatorDate = DateTime.Now;
        await DbSet.AddAsync(t);
        await Context.SaveChangesAsync();
    }

    public async Task Edit(T t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }

    public async Task Edit(List<T> t)
    {
        foreach (var baseEntity in t)
        {
            Context.Entry(baseEntity).State = EntityState.Modified;
        }
        await Context.SaveChangesAsync();
    }

    public async Task Delete(Guid id)
    {
        var entity = GetById(id);
        DbSet.Remove(entity);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(List<Guid> ids)
    {
        var listEntity = await GetByIds(ids);
        DbSet.RemoveRange(listEntity);
        await Context.SaveChangesAsync();
    }

}