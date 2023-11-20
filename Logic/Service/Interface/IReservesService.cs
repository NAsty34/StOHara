using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface IReservesService
{
    public Task<List<ReserveEntity>> Create(List<ReserveEntity> t);
    public List<ReserveEntity> GetAllReserve();
    public Task<ReserveEntity> Create(ReserveEntity t);
    public Task<ReserveEntity> Edit(ReserveEntity t);
    public Task<bool> CheckHashReservationBetweenTime(DateTime startTime, DateTime endTime, List<string> tablesId);
    public Task Delete();
    public Task Delete(Guid reservId);
    public Task Delete (ReserveEntity? bookingEnity);
    public void Delete(List<ReserveEntity> bookingEnity);
    public IEnumerable<ReserveEntity> GetBookingByInterval(DateTime interval);
    public Task<ReserveEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id);
    public Task<ReserveEntity?> GetByPaymentId(string paymentId);
    public Task<ReserveEntity?> GetById(Guid id);
}