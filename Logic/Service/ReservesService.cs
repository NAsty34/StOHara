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
    
    public List<ReservesEntity> GetAllReserve()
    {
        return _reservesRepository.GetAllReserves();
    }

    public async Task<List<ReservesEntity>> Create(List<ReservesEntity> t)
    {
        await _reservesRepository.Create(t);
        return t;
    }
    public async Task<ReservesEntity> Create(ReservesEntity t)
    {
        await _reservesRepository.Create(t);
        return t;
    }

    public async Task Delete()
    {
        await _reservesRepository.Delete();
    }
    public async Task Delete(Guid reservId)
    {
        await _reservesRepository.Delete(reservId);
    }
    public IEnumerable<ReservesEntity> GetBookingByInterval(DateTime interval)
    {
        return _reservesRepository.GetByInterval(interval);
    }
    public async Task<ReservesEntity> GetByPaymentId(Guid paymentId, Dictionary<string, string> id)
    {
        var tId = await _reservesRepository.GetByPaymentId(paymentId, id);
        return tId;
    }
    public async Task<ReservesEntity?> GetByPaymentId(string paymentId)
    {
        var tId = await _reservesRepository.GetByPaymentId(Guid.Parse(paymentId));
        return tId;
    }
    public async Task<ReservesEntity> Edit(ReservesEntity t)
    {
        await _reservesRepository.Edit(t);
        return t;
    }
    public async Task Delete(ReservesEntity? bookingEnity)
    {
        await _reservesRepository.Delete(bookingEnity);
    }
    public void Delete(List<ReservesEntity> bookingEnity)
    {
        _reservesRepository.Delete(bookingEnity);
    }
    public async Task<ReservesEntity?> GetById(Guid id)
    {
        var tId = await _reservesRepository.GetById(id);
        return tId;
    }
}