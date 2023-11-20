using Data.Model;
using Data.Model.Entities;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class ClientRepository:IClientRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<ClientEnity> DbSet;

    public ClientRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = Context.Set<ClientEnity>();
    }

    public async Task<PageModel<ClientEnity>> GetPage(int? page, int? size)
    {
        return await DbSet.GetPage(page, size);
    }
    
    public async Task<ClientEnity?> GetByFio(string name, string surname, string patronymic)
    {
        return await DbSet.FirstOrDefaultAsync(a=>a.Name == name && a.Surname == surname && a.Patronymic == patronymic);
    }
    
    public async Task<ClientEnity?> GetById(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(a => a.Id == id);
    }
    
    
    public async Task Create(ClientEnity t)
    {
        await DbSet.AddAsync(t);
        await Context.SaveChangesAsync();
    }
    
    public async Task Delete()
    {
        var range = DbSet.Where(a => true).ToList();
        DbSet.RemoveRange(range);
        await Context.SaveChangesAsync();
    }
    
    
}