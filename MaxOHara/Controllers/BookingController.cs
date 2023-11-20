using System.Net;
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
    private readonly ILogger<ReserveEntity> _logger;
    private readonly ForIIKO _iiko;
    private readonly BookingOptions _bookingOptions;
    private readonly ISendEmailService _sendEmailService;

    public BookingController(IClientService clientService, ITablesService tablesService, IIikoService aikoService,
        IReservesService reservesService, ILogger<ReserveEntity> logger, IOptions<ForIIKO> iiko,
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
        List<TableEntity> tables;

        if (hall == "launge")
        {
            tables = await _tablesService.GetTablesLounge();
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
    public async Task<ResponseDto<string>> CreateReserve([FromBody] CreateReservesDto create)
    {
        var id = Guid.NewGuid();
        var price = 0;
        var returnUrl = _bookingOptions.ReturnUrl;
        var clientsDb = await _clientService.GetByFio(create.Name, create.Surname, create.Patronymic);
        var client = new ClientEnity();
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

        if (create.TableIds == null || create.TableIds.Count==0)
        {
            throw new Exception("нет столов");
        }
        var tables = _tablesService.GetByIds(create.TableIds);
        if (tables.Count==0)
        {
            throw new Exception("нет столов");
        }

        var reserve = new ReserveEntity
        {
            Id = id,
            Status = StatusEntity.Progress,
            EstimatedStartTime = create.EstimatedStartTime,
            DurationInMinutes = create.DurationInMinutes,
            GuestsCount = create.GuestsCount,
            Client = client,
            Tables = tables
        };

        if (create.EstimatedStartTime < DateTime.Now)
        {
            throw new PastDateException();
        }

        var startTime = create.EstimatedStartTime;
        var endTime = startTime.AddMinutes(create.DurationInMinutes);

        if (create.EstimatedStartTime.Hour < 12 || await _reservesService.CheckHashReservationBetweenTime(startTime, endTime, create.TableIds))
        {
            throw new HoursErrorException();
        }
        
        await _reservesService.Create(reserve);

        var idsTablesDb = _tablesService.GetByAll().Select(a => a.Id).ToList();
        var idsData = create.TableIds.Select(a => a.Split(',')).ToList();
        var selectTables = new List<TableEntity>();
        foreach (var arrayIds in idsData)
        {
            selectTables.AddRange(from tableId in arrayIds
                where idsTablesDb.Contains(Guid.Parse(tableId))
                select _tablesService.GetById(tableId));
        }

        var reserveTables = new List<ReserveEntity>();
        foreach (var table in create.TableIds.Select(tableId => _tablesService.GetById(tableId)))
        {
            reserveTables.Add(reserve);
            table.IsReserve = true;
            table.Reserves = reserveTables;
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

            /*var payment = await _clientService.CreatePayment(price, id);
            reserve.PaymentId = Guid.Parse(payment.Id);*/
            await _reservesService.Edit(reserve);
            /*returnUrl = payment.Confirmation.ConfirmationUrl;*/
        }
        else
        {
            reserve.Status = StatusEntity.Success;
            /*var reserveModel = new
            {
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
                tableIds = reserve.Tables,
                estimatedStartTime = reserve.EstimatedStartTime,
                transportToFrontTimeout = 0,
                guests = new
                {
                    count = reserve.GuestsCount
                }
            };
            var bodyReserve = JsonConvert.SerializeObject(reserveModel);
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/create",
                bodyReserve);*/
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
        /*var client = _clientService.CreateClient();
        var message = Yandex.Checkout.V3.Client.ParseMessage(Request.Method, Request.ContentType, body.ToString());
        var payment = message.Object;
        if (message.Event == Event.PaymentWaitingForCapture)
        {
            var paymentDb = await _reservesService.GetByPaymentId(payment.Id);
            var editListTable = new List<TableEntity>();
            if (paymentDb != null)
            {
                var tables = _tablesService.GetByIds(paymentDb.Tables.Select(a=>a.Id.ToString()).ToList()).ToList();
                paymentDb.Status = StatusEntity.Success;
                await _reservesService.Edit(paymentDb);
                foreach (var table in tables)
                {
                    table.IsReserve = true;
                    /*table.Reserves = paymentDb;#1#
                    editListTable.Add(table);
                }

                await _tablesService.Edit(editListTable);
                client.CapturePayment(payment.Id);
                var reserveModel = new
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
                    tableIds = paymentDb.Tables,
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
        }*/
    }

    [Route("/api/v1/reserves/Iiko")]
    [HttpPost]
    public async Task ReservesIiko([FromBody] dynamic data)
    {
        /*var body = JsonConvert.DeserializeObject<dynamic>(data.ToString());
        bool isDeleted = body[0].eventInfo.isDeleted;
        ReserveEntity? reserveDb = await _reservesService.GetById(Guid.Parse(body[0].eventInfo.id.ToString()));

        if (!isDeleted)
        {
            if (reserveDb == null)
            {
                var newReserve = new ReserveEntity()
                {
                    Id = Guid.Parse(body[0].eventInfo.id.ToString()),
                    Status = body[0].eventInfo.creationStatus,
                    PaymentId = null,
                    /*Tables = ((JArray)body[0].eventInfo.reserve.tableIds).Select(v => v.ToString()).ToList(),#1#
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
                /*reserveDb.Tables = ((JArray)body[0].eventInfo.reserve.tableIds).Select(v => v.ToString()).ToList();#1#
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
                var tables = _tablesService.GetByIds(reserveDb.Tables.Select(a=>a.Id.ToString()).ToList());
                foreach (var table in tables)
                {
                    table.IsReserve = false;
                    table.Reserves = null;
                    await _tablesService.Edit(table);
                }

                await _reservesService.Delete(reserveDb);
            }
        }*/
    }
}