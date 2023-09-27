using Data.Model.Entities;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Data.Repository;

public class ReservesRepository:IReservesRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<ReservesEntity> DbSet;
    private ILogger<ReservesEntity> _logger;

    public ReservesRepository(MaxOHaraContext context, ILogger<ReservesEntity> logger)
    {
        Context = context;
        _logger = logger;
        DbSet = DbSet = Context.Set<ReservesEntity>();;
    }
    
    public async Task<ReservesEntity?> GetById(Guid id)
    {
        return DbSet.FirstOrDefault(a => a.Id == id);
    }
    
    public List<ReservesEntity> GetAllReserves()
    {
        return DbSet.Where(a => true).ToList();
    }
    
    public async Task Create(List<ReservesEntity> t)
    {
        foreach (var oneReserv in t)
        {
            oneReserv.CreatorDate = DateTime.Now;
        }
        await DbSet.AddRangeAsync(t);
        await Context.SaveChangesAsync();
    }
    public async Task Create(ReservesEntity t)
    {
        t.CreatorDate = DateTime.Now;
        await DbSet.AddAsync(t);
        await Context.SaveChangesAsync();
    }

    public async Task Delete()
    {
        var range = DbSet.Where(a => true).ToList();
        DbSet.RemoveRange(range);
        await Context.SaveChangesAsync();
    }
    public IEnumerable<ReservesEntity> GetByInterval(DateTime interval)
    {
        _logger.Log(LogLevel.Information, "========Interval=======" + interval);
        return DbSet.Where(a => a.CreatorDate <= interval && a.Status == StatusEntity.Progress);
    }
    public async Task<ReservesEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id)
    {
       return await DbSet.FirstAsync(a => a.PaymentId == paymentId && a.Id == new Guid(id.Values.First()));
    }
    public async Task<ReservesEntity?> GetByPaymentId(Guid paymentId)
    {
        return await DbSet.FirstOrDefaultAsync(a => a.PaymentId == paymentId );
    }
    public void Delete(List<ReservesEntity> t)
    {
        DbSet.RemoveRange(t);
        Context.SaveChanges();
    }
    public async Task Delete(ReservesEntity t)
    {
        DbSet.Remove(t);
        await Context.SaveChangesAsync();
    }
    public async Task Delete(Guid reservId)
    {
        var reserv = DbSet.First(a => a.Id == reservId);
        DbSet.Remove(reserv);
        await Context.SaveChangesAsync();
    }
    public async Task Edit(ReservesEntity t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }
}