/*using Data.Model.Entities;
using Logic.Service.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Quartz;
using Yandex.Checkout.V3;

namespace MaxOHara;

public class CheckPaymentStatus : IJob
{
    private readonly IReservesService _reservesService;
    private readonly ITablesService _tablesService;
    private readonly ILogger<CheckPaymentStatus> _logger;
    private readonly IClientService _clientService;

    public CheckPaymentStatus(IReservesService reservesService, ILogger<CheckPaymentStatus> logger, ITablesService tablesService, IClientService clientService)
    {
        _reservesService = reservesService;
        _logger = logger;
        _tablesService = tablesService;
        _clientService = clientService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.Log(LogLevel.Information, "WORKCheckPayment");
        var interval = DateTime.Now.AddMinutes(-10);//10minutes
        var listBooking = _reservesService.GetBookingByInterval(interval).ToList();

        if (listBooking != null)
        {
            foreach (var table in from reserves in listBooking from tableId in reserves.TableIds select _tablesService.GetById(tableId))
            {
                if (table != null)
                {
                    table.IsReserve = false;
                    table.Reserve = null;    
                }
            }

            _reservesService.Delete(listBooking);
        }
        
        
        return Task.CompletedTask;
    }
}*/