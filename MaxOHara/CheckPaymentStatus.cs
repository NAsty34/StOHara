using Data.Repository.Interface;
using Logic.Service.Interface;
using Quartz;

namespace MaxOHara;

public class CheckPaymentStatus : IJob
{
    private readonly IReservesService _reservesService;
    private readonly ITablesService _tablesService;

    public CheckPaymentStatus(IReservesService reservesService, ITablesService tablesService)
    {
        _reservesService = reservesService;
        _tablesService = tablesService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var interval = DateTime.Now.AddMinutes(-10);//10minutes
        var listBooking = _reservesService.GetBookingByInterval(interval).ToList();

        foreach (var table in from reserves in listBooking from tableEntity in reserves.Tables select _tablesService.GetById(tableEntity.Id.ToString()))
        {
            if (table != null)
            {
                table.IsReserve = false;
                table.Reserves = null;    
            }
        }

        _reservesService.Delete(listBooking);


        return Task.CompletedTask;
    }
}