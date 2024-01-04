using Data.Model.Entities;
using Data.Repository.Interface;
using Logic.Service.Interface;

namespace Logic.Service;

public class ReservesService:IReservesService
{
    private readonly IReservesRepository _reservesRepository;

    public ReservesService(IReservesRepository reservesRepository)
    {
        _reservesRepository = reservesRepository;
    }
    
    public List<ReserveEntity> GetAllReserve()
    {
        return _reservesRepository.GetAllReserve();
    }

    public async Task<List<ReserveEntity>> Create(List<ReserveEntity> t)
    {
        await _reservesRepository.Create(t);
        return t;
    }
    public async Task<ReserveEntity> Create(ReserveEntity t)
    {
        await _reservesRepository.Create(t);
        return t;
    }

    public async Task<bool> CheckHashReservationBetweenTime(DateTime startTime, DateTime endTime, List<string> tablesId)
    {
        return await _reservesRepository.CheckHashReservationBetweenTime(startTime, endTime, tablesId);
    }

    public async Task Delete()
    {
        await _reservesRepository.Delete();
    }
    public async Task Delete(Guid reserveId)
    {
        await _reservesRepository.Delete(reserveId);
    }
    public IEnumerable<ReserveEntity> GetBookingByInterval(DateTime interval)
    {
        return _reservesRepository.GetByInterval(interval);
    }
    public async Task<ReserveEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id)
    {
        var tId = await _reservesRepository.GetByPaymentId(paymentId, id);
        return tId;
    }
    public async Task<ReserveEntity?> GetByPaymentId(string paymentId)
    {
        var tId = await _reservesRepository.GetByPaymentId(Guid.Parse(paymentId));
        return tId;
    }
    public async Task<ReserveEntity> Edit(ReserveEntity t)
    {
        await _reservesRepository.Edit(t);
        return t;
    }
    public async Task Delete(ReserveEntity? bookingEntity)
    {
        await _reservesRepository.Delete(bookingEntity);
    }
    public void Delete(List<ReserveEntity> bookingEntity)
    {
        _reservesRepository.Delete(bookingEntity);
    }
    public async Task<ReserveEntity?> GetById(Guid id)
    {
        var tId = await _reservesRepository.GetById(id);
        return tId;
    }
}