using Data.Model;
using Data.Model.Entities;
using Yandex.Checkout.V3;

namespace Logic.Service.Interface;

public interface IClientService
{
    public Task<ClientEnity?> GetByFio(string name, string surname, string patronymic);
    public Task<PageModel<ClientEnity>> GetPage(int? page, int? size);
    public Task<ClientEnity?> GetById(Guid id);
    
    Task<ClientEnity> Create(ClientEnity t);
    public Task Delete();
    public Client CreateClient();
    public Task<Payment?> CreatePayment(decimal value, Guid id);
    public Task<PaymentStatus> GetStatusPayment(string id);
    public Task<PaymentStatus> MyStatusPayment();

}