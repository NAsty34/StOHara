using Logic.Service.Interface;
using Quartz;

namespace MaxOHara;

public class CheckReserveDateTime : IJob
{
    private readonly IReservesService _reservesService;
    private readonly ITablesService _tablesService;

    public CheckReserveDateTime(IReservesService reservesService, ITablesService tablesService)
    {
        _reservesService = reservesService;
        _tablesService = tablesService;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var reservesToday = _reservesService.GetAllReserve();
        foreach (var reserve in reservesToday.Where(reserve => reserve.EstimatedStartTime.AddMinutes(reserve.DurationInMinutes) < DateTime.Today))
        {
            var tables = _tablesService.GetByIds(reserve.Tables.Select(a=>a.Id.ToString()).ToList());
            foreach (var table in tables)
            {
                table.IsReserve = false;
                table.Reserves = null;
                _tablesService.Edit(table);
            }
            _reservesService.Delete(reserve);
        }

        return Task.CompletedTask;
    }
}