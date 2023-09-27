using Data.Model.Entities;

namespace Logic.Service.Interface;

public interface ISendEmailService
{
    Task Send(string email, ReservesEntity reserve);
}