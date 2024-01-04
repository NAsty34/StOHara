using Data.Model;
using Data.Model.Entities;
using Data.Model.Options;
using Data.Repository.Interface;
using Logic.Service.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Yandex.Checkout.V3;

namespace Logic.Service;

public class ClientService:IClientService
{
    private readonly IClientRepository _clientRepository;
    private readonly BookingOptions _bookingOptions;
    public PaymentStatus Status;
    private readonly ILogger<ClientService> _logger;
    public ClientService(IClientRepository clientRepository, IOptions<BookingOptions> bookingOptions, ILogger<ClientService> logger)
    {
        _clientRepository = clientRepository;
        _logger = logger;
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
    
    public async Task<Payment?> CreatePayment(ReserveEntity reserveEntity, decimal value, Guid id)
    {
        _logger.Log(LogLevel.Information, value + "");
        _logger.Log(LogLevel.Information, _bookingOptions.Currency);
        var dictionaryMetadata = new Dictionary<string, string>
        {
            {"id", $"{id.ToString()}"}
        };

        var client = CreateClient();
        var newPayment = new Payment
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
            Metadata = dictionaryMetadata,
            Receipt = new Receipt
            {
                Customer = new Customer
                {
                    Email = reserveEntity.Client.Email
                },
                Items = new List<ReceiptItem>
                {
                    new ReceiptItem
                    {
                        Description = "Бронирование столика",
                        Amount = new Amount
                        {
                            Value = value,
                            Currency = _bookingOptions.Currency
                        },
                        VatCode = VatCode.NoVat,
                        Quantity = 1,
                        PaymentSubject = PaymentSubject.Service,
                        PaymentMode = PaymentMode.FullPrepayment
                    }
                }
            }
        };
        try
        {
            var payment = client.CreatePayment(newPayment);
            return payment;
        }
        catch (YandexCheckoutException e)
        {
            _logger.Log(LogLevel.Information, e.Message);
            _logger.Log(LogLevel.Information, e.Error.Description);
            _logger.Log(LogLevel.Information, e.Error.Parameter);
            _logger.Log(LogLevel.Information, e.Error.ToString());
            throw;
        }
    }
    

}