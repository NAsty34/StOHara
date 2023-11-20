using Data.Model.Entities;
using Data.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Data.Repository;

public class ReservesRepository : IReservesRepository
{
    protected readonly MaxOHaraContext Context;
    protected readonly DbSet<ReserveEntity> DbSet;

    public ReservesRepository(MaxOHaraContext context)
    {
        Context = context;
        DbSet = DbSet = Context.Set<ReserveEntity>();
    }

    public List<ReserveEntity> GetAllReserve()
    {
        return DbSet.Where(a => true).ToList();
    }
    public async Task<ReserveEntity?> GetById(Guid id)
    {
        return await DbSet.FirstOrDefaultAsync(a => a.Id == id);
    }
    
    public async Task<List<ReserveEntity>> GetByIds(List<Guid> ids)
    {
        return await DbSet.Where(a => ids.Equals(a.Id)).ToListAsync();
    }
    
    public async Task<bool> CheckHashReservationBetweenTime(DateTime startTime, DateTime endTime, List<string> tablesId)
    {
        var ids = tablesId.Select(Guid.Parse);

        var result = await DbSet.FirstOrDefaultAsync(entity => entity.Tables.Any(t => ids.Contains(t.Id)) &&
                                                               ((entity.EstimatedStartTime >= startTime && entity.EstimatedStartTime <= endTime) || 
                                                                (entity.EstimatedStartTime.AddMinutes(entity.DurationInMinutes) >= startTime && entity.EstimatedStartTime.AddMinutes(entity.DurationInMinutes) <= endTime)));
        return result != null;
    }

    public async Task Create(List<ReserveEntity> t)
    {
        foreach (var oneReserve in t)
        {
            oneReserve.CreatorDate = DateTime.Now;
        }

        await DbSet.AddRangeAsync(t);
        await Context.SaveChangesAsync();
    }

    public async Task Create(ReserveEntity t)
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

    public IEnumerable<ReserveEntity> GetByInterval(DateTime interval)
    {
        /*_logger.Log(LogLevel.Information, "========Interval=======" + interval);*/
        return DbSet.Where(a => a.CreatorDate <= interval && a.Status == StatusEntity.Progress);
    }

    public async Task<ReserveEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id)
    {
        return await DbSet.FirstAsync(a => a.PaymentId == paymentId && a.Id == new Guid(id.Values.First()));
    }

    public async Task<ReserveEntity?> GetByPaymentId(Guid paymentId)
    {
        return await DbSet.FirstOrDefaultAsync(a => a.PaymentId == paymentId);
    }

    public void Delete(List<ReserveEntity> t)
    {
        DbSet.RemoveRange(t);
        Context.SaveChanges();
    }

    public async Task Delete(ReserveEntity t)
    {
        DbSet.Remove(t);
        await Context.SaveChangesAsync();
    }

    public async Task Delete(Guid reserveId)
    {
        var reserve = DbSet.First(a => a.Id == reserveId);
        DbSet.Remove(reserve);
        await Context.SaveChangesAsync();
    }

    public async Task Edit(ReserveEntity t)
    {
        Context.Entry(t).State = EntityState.Modified;
        await Context.SaveChangesAsync();
    }
}