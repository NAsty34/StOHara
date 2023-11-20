using Data.Model;
using Data.Model.Entities;
using Data.Model.Options;
using Data.Repository.Interface;
using Logic.Service.Interface;
using Microsoft.Extensions.Options;
using Yandex.Checkout.V3;

namespace Logic.Service;

public class ClientService:IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly BookingOptions _bookingOptions;
    public PaymentStatus Status;
    public ClientService(IClientRepository clientRepository, IOptions<BookingOptions> bookingOptions)
    {
        _clientRepository = clientRepository;
        _bookingOptions = bookingOptions.Value;
    }
    
    public async Task<ClientEnity?> GetByFio(string name, string surname, string patronymic)
    {
        return await _clientRepository.GetByFio(name, surname, patronymic);
    }
    
    public async Task<PageModel<ClientEnity>> GetPage(int? page, int? size)
    {
        return await _clientRepository.GetPage(page, size);
    }

    public async Task<ClientEnity?> GetById(Guid id)
    {
        var tId = await _clientRepository.GetById(id);
        return tId;
    }
    

    public async Task<ClientEnity> Create(ClientEnity t)
    {
        await _clientRepository.Create(t);
        return t;
    }
   
    public async Task Delete()
    {
        await _clientRepository.Delete();
    }

    public Client CreateClient()
    {
        return new Client(
            shopId: _bookingOptions.ShopId, 
            secretKey: _bookingOptions.SecretKey);
    }

    public PaymentStatus GetStatusPayment(string id)
    {
        var client = CreateClient();
        var payment = client.GetPayment(id);
        Status = payment.Status;
        return Status;
    }
    
    public PaymentStatus MyStatusPayment()
    {
        return Status;
    }
    
    public async Task<Payment?> CreatePayment(decimal value, Guid id)
    {
        var dictionaryMetadata = new Dictionary<string, string>
        {
            {"id", $"{id.ToString()}"}
        };

        var client = CreateClient();
        var newPayment = new NewPayment
        {
            Amount = new Amount
            {
                Value = value,
                Currency = _bookingOptions.Currency
            },
            Confirmation = new Confirmation
            {
                Type = ConfirmationType.Redirect,
                ReturnUrl = _bookingOptions.ReturnUrlForPayment + id
            },
            Metadata = dictionaryMetadata
        };
        var payment = client.CreatePayment(newPayment);
        
        return payment;
    }
    

}