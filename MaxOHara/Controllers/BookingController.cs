/*using System.Net;
using System.Net.Http.Headers;
using Data.Model;
using Data.Model.Entities;
using Data.Model.Json;
using Data.Model.Options;
using Logic.Exceptions;
using Logic.Service.Interface;
using MaxOHara.Dto;
using MaxOHara.Dto.BookingDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Yandex.Checkout.V3;

namespace MaxOHara.Controllers;

public class BookingController : Controller
{
    private readonly IClientService _clientService;
    static readonly HttpClient Client = new();
    private readonly ITablesService _tablesService;
    private readonly IIikoService _aikoService;
    private readonly IReservesService _reservesService;
    private readonly ILogger<ReservesEntity> _logger;
    private readonly ForIIKO _iiko;
    private readonly BookingOptions _bookingOptions;
    private readonly ISendEmailService _sendEmailService;

    public BookingController(IClientService clientService, ITablesService tablesService, IIikoService aikoService,
        IReservesService reservesService, ILogger<ReservesEntity> logger, IOptions<ForIIKO> iiko,
        IOptions<BookingOptions> bookingOptions, ISendEmailService sendEmailService)
    {
        _clientService = clientService;
        _tablesService = tablesService;
        _aikoService = aikoService;
        _reservesService = reservesService;
        _logger = logger;
        _sendEmailService = sendEmailService;
        _bookingOptions = bookingOptions.Value;
        _iiko = iiko.Value;
    }

    [Route("/api/v1/booking/table/{hall}")]
    [HttpGet]
    public async Task<ResponseDto<IEnumerable<TablesDto>>> GetTables(string hall)
    {
        List<TablesEntity> tables;

        if (hall == "launge")
        {
            tables = await _tablesService.GetTablesLaunge();
        }
        else
        {
            tables = await _tablesService.GetTablesHall();
        }

        return new ResponseDto<IEnumerable<TablesDto>>(tables.Select(entity => new TablesDto(entity)));
    }

    [Route("/api/v1/booking/{id}")]
    [HttpGet]
    public async Task<ReserveDto> GetReserve(Guid id)
    {
        var reserve = await _reservesService.GetById(id);
        if (reserve != null)
        {
            return new ReserveDto(reserve);
        }

        throw new TimeExpiredException();
    }

    [Route("/api/v1/booking")]
    [HttpPost]
    public async Task<ResponseDto<string>> CreateBooking([FromBody] CreateReservesDto create)
    {
        var id = Guid.NewGuid();
        var price = 0;
        var returnUrl = _bookingOptions.ReturnUrl;
        var clientsDb = await _clientService.GetByFio(create.Name, create.Surname, create.Patronymic);
        ClientEnity client;
        if (clientsDb == null)
        {
            client = new ClientEnity
            {
                Id = Guid.NewGuid(),
                Name = create.Name,
                Surname = create.Surname,
                Patronymic = create.Patronymic,
                Phone = create.Phone,
                Message = create.Message,
                Email = create.Email
            };
            await _clientService.Create(client);
        }
        else
        {
            client = clientsDb;
        }


        var reserve = new ReservesEntity
        {
            Id = id,
            Status = StatusEntity.Progress,
            TableIds = create.TableIds,
            EstimatedStartTime = create.EstimatedStartTime,
            DurationInMinutes = create.DurationInMinutes,
            GuestsCount = create.GuestsCount,
            Client = client
        };

        if (create.EstimatedStartTime < DateTime.Now)
        {
            throw new PastDateException();
        }

        if (create.TableIds != null)
        {
            var selectTables = _tablesService.GetByIds(create.TableIds);

            if (selectTables.Select(table => table.Reserve?.EstimatedStartTime.TimeOfDay ?? default).Any(a =>
                {
                    var reserveMinutes = reserve.EstimatedStartTime.TimeOfDay.Hours * 60 +
                                         reserve.EstimatedStartTime.TimeOfDay.Minutes;
                    var aMinutes = a.Hours * 60 + a.Minutes;

                    return Math.Abs(reserveMinutes - aMinutes) < 120;
                }))
            {
                throw new HoursErrorException();
            }
        }

        await _reservesService.Create(reserve);

        foreach (var table in reserve.TableIds.Select(tableId => _tablesService.GetById(tableId)))
        {
            table.IsReserve = true;
            table.Reserve = reserve;
            await _tablesService.Edit(table);
        }
        
        if (create.EstimatedStartTime.DayOfWeek is DayOfWeek.Friday or DayOfWeek.Saturday &&
            create.EstimatedStartTime.Hour >= 18)
        {
            foreach (var table in create.TableIds.Select(tableId => _tablesService.GetById(tableId)))
            {
                if (table.Number is 5 or 6 or 7)
                {
                    price += 10;
                }
                else
                {
                    price += 10;
                }
            }

            var payment = await _clientService.CreatePayment(price, id);
            reserve.PaymentId = Guid.Parse(payment.Id);
            await _reservesService.Edit(reserve);
            returnUrl = payment.Confirmation.ConfirmationUrl;
        }
        else
        {
            reserve.Status = StatusEntity.Success;
            /*_logger.Log(LogLevel.Information, "========CreateIiko===========");*/
/*            var reserveModel = new
            {
                organizationId = _iiko.organizationId,
                terminalGroupId = _iiko.terminalGroupId,
                id = "",
                externalNumber = "",
                order = "",
                customer = new
                {
                    id = client.Id.ToString(),
                    name = client.Name + " " + client.Surname,
                    surname = client.Patronymic,
                    comment = "",
                    birthdate = "",
                    email = client.Email,
                    shouldReceiveOrderStatusNotifications = true,
                    gender = "NotSpecified",
                    type = "regular"
                },
                phone = client.Phone,
                comment = client.Message,
                durationInMinutes = 120,
                shouldRemind = false,
                tableIds = reserve.TableIds,
                estimatedStartTime = reserve.EstimatedStartTime,
                transportToFrontTimeout = 0,
                guests = new
                {
                    count = reserve.GuestsCount
                }
            };
            var bodyReserve = JsonConvert.SerializeObject(reserveModel);
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/create",
                bodyReserve);
        }

        reserve.Price = price;
        await _reservesService.Edit(reserve);

        await _sendEmailService.Send(create.Email, reserve);
        
        return new ResponseDto<string>(returnUrl);
    }
    
    


    [Route("/api/v1/payment")]
    [HttpPost]
    public async Task Payment([FromBody] object body)
    {
        _logger.Log(LogLevel.Information, "=====Message====" + body);
        var client = _clientService.CreateClient();
        var message = Yandex.Checkout.V3.Client.ParseMessage(Request.Method, Request.ContentType, body.ToString());
        var payment = message.Object;
        /*_logger.Log(LogLevel.Information, "======PaumentIf===========");*/
/*        if (message.Event == Event.PaymentWaitingForCapture)
        {
            var paymentDb = await _reservesService.GetByPaymentId(payment.Id);
            var editListTable = new List<TablesEntity>();
            /*_logger.Log(LogLevel.Information, "=====PaymentDB=====" + paymentDb.Id);*/
/*            if (paymentDb != null)
            {
                var tables = _tablesService.GetByIds(paymentDb.TableIds).ToList();
                paymentDb.Status = StatusEntity.Success;
                await _reservesService.Edit(paymentDb);
                /*_logger.Log(LogLevel.Information, "======Ok===========");*/
/*                foreach (var table in tables)
                {
                    table.IsReserve = true;
                    table.Reserve = paymentDb;
                    editListTable.Add(table);
                }

                await _tablesService.Edit(editListTable);
                client.CapturePayment(payment.Id);
                /*_logger.Log(LogLevel.Information, "======PaymentOkStatus=======" + _status);*/
/*                var reserveModel = new
                {
                    organizationId = "df80b1df-07ee-42eb-b271-bea261bc2707",
                    terminalGroupId = "e7fea5ed-e9ca-1c85-0163-ca1dfb1100c3",
                    id = "",
                    externalNumber = "",
                    order = "",
                    customer = new
                    {
                        id = paymentDb.Client.Id.ToString(),
                        name = paymentDb.Client.Name + " " + paymentDb.Client.Surname,
                        surname = paymentDb.Client.Patronymic,
                        comment = "",
                        birthdate = "",
                        email = "",
                        shouldReceiveOrderStatusNotifications = true,
                        gender = "NotSpecified",
                        type = "regular"
                    },
                    phone = paymentDb.Client.Phone,
                    comment = paymentDb.Client.Message,
                    durationInMinutes = 120,
                    shouldRemind = false,
                    tableIds = paymentDb.TableIds,
                    estimatedStartTime = paymentDb.EstimatedStartTime,
                    transportToFrontTimeout = 0,
                    guests = new
                    {
                        count = paymentDb.GuestsCount
                    }
                };
                var bodyReserve = JsonConvert.SerializeObject(reserveModel);
                await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/create",
                    bodyReserve);
            }
            else
            {
                client.CancelPayment(payment.Id);
            }
        }
    }

    [Route("/api/v1/reserves/Iiko")]
    [HttpPost]
    public async Task ReservesIiko([FromBody] dynamic data)
    {
        _logger.Log(LogLevel.Information, "==========Body===========" + (object)data.ToString());
        dynamic body = JsonConvert.DeserializeObject<dynamic>(data.ToString());
        _logger.Log(LogLevel.Information, "==========Body2===========" + (object)body.ToString());
        bool isDeleted = body[0].eventInfo.isDeleted;
        ReservesEntity? reserveDb = await _reservesService.GetById(Guid.Parse(body[0].eventInfo.id.ToString()));

        if (!isDeleted)
        {
            if (reserveDb == null)
            {
                var newReserve = new ReservesEntity()
                {
                    Id = Guid.Parse(body[0].eventInfo.id.ToString()),
                    Status = body[0].eventInfo.creationStatus,
                    PaymentId = null,
                    TableIds = ((JArray)body[0].eventInfo.reserve.tableIds).Select(v => v.ToString()).ToList(),
                    EstimatedStartTime = DateTime.Parse(body[0].eventInfo.reserve.estimatedStartTime.ToString()),
                    DurationInMinutes = int.Parse(body[0].eventInfo.reserve.durationInMinutes.ToString()),
                    GuestsCount = body[0].eventInfo.reserve.guestsCount,
                    CreatorDate = DateTime.Parse(body[0].eventTime.ToString())
                };
                await _reservesService.Create(newReserve);
            }
            else
            {
                reserveDb.Id = Guid.Parse(body[0].eventInfo.id.ToString());
                reserveDb.Status = body[0].eventInfo.creationStatus;
                reserveDb.PaymentId = null;
                reserveDb.TableIds = ((JArray)body[0].eventInfo.reserve.tableIds).Select(v => v.ToString()).ToList();
                reserveDb.EstimatedStartTime = DateTime.Parse(body[0].eventInfo.reserve.estimatedStartTime.ToString());
                reserveDb.DurationInMinutes = int.Parse(body[0].eventInfo.reserve.durationInMinutes.ToString());
                reserveDb.GuestsCount = body[0].eventInfo.reserve.guestsCount;
                reserveDb.CreatorDate = DateTime.Parse(body[0].eventTime.ToString());
                await _reservesService.Edit(reserveDb);
            }    
        }
        else
        {
            if (reserveDb != null)
            {
                var tables = _tablesService.GetByIds(reserveDb.TableIds);
                foreach (var table in tables)
                {
                    table.IsReserve = false;
                    table.Reserve = null;
                    await _tablesService.Edit(table);
                }
                await _reservesService.Delete(reserveDb);
            }
        }
    }
}*/