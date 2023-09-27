using Data.Model.Entities;

namespace Data.Repository.Interface;

public interface IReservesRepository
{
    public Task Create(List<ReservesEntity> t);
    public List<ReservesEntity> GetAllReserves();
    public Task<ReservesEntity?> GetById(Guid id);
    public Task Create(ReservesEntity t);
    public Task Edit(ReservesEntity t);
    public Task Delete();
    public Task Delete(ReservesEntity t);
    public void Delete(List<ReservesEntity> t);
    public Task Delete(Guid reservId);
    public IEnumerable<ReservesEntity> GetByInterval(DateTime interval);
    public Task<ReservesEntity?> GetByPaymentId(Guid paymentId);
    public Task<ReservesEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id);
}