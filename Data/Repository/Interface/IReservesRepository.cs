using Data.Model.Entities;

namespace Data.Repository.Interface;

public interface IReservesRepository
{
    public Task Create(List<ReserveEntity> t);
    public List<ReserveEntity> GetAllReserve();
    
    public Task<ReserveEntity?> GetById(Guid id);
    Task<List<ReserveEntity>> GetByIds(List<Guid> ids);
    public Task Create(ReserveEntity t);
    public Task<Boolean> CheckHashReservationBetweenTime(DateTime startTime, DateTime endTime, List<string> tablesId);
    public Task Edit(ReserveEntity t);
    public Task Delete();
    public Task Delete(ReserveEntity t);
    public void Delete(List<ReserveEntity> t);
    public Task Delete(Guid reserveId);
    public IEnumerable<ReserveEntity> GetByInterval(DateTime interval);
    public Task<ReserveEntity?> GetByPaymentId(Guid paymentId);
    public Task<ReserveEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id);
}