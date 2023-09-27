using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface IReservesService
{
    public List<ReservesEntity> GetAllReserve();
    public Task<List<ReservesEntity>> Create(List<ReservesEntity> t);
    public Task<ReservesEntity> Create(ReservesEntity t);
    public Task<ReservesEntity> Edit(ReservesEntity t);
    public Task Delete();
    public Task Delete(Guid reservId);
    public Task Delete (ReservesEntity? bookingEnity);
    public void Delete(List<ReservesEntity> bookingEnity);
    public IEnumerable<ReservesEntity> GetBookingByInterval(DateTime interval);
    public Task<ReservesEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id);
    public Task<ReservesEntity?> GetByPaymentId(string paymentId);
    public Task<ReservesEntity?> GetById(Guid id);
}