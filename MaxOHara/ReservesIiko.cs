using System.Net.Http.Headers;
using Data.Model.Entities;
using Data.Model.Json;
using Data.Model.Options;
using Logic.Service.Interface;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Quartz;

namespace MaxOHara;

public class ReservesIiko : IJob
{
    private static readonly HttpClient Client = new();
    private readonly ITablesService _tablesService;
    private readonly IIikoService _aikoService;
    private readonly IReservesService _reservesService;
    private readonly IClientService _clientService;
    private readonly ForIIKO _aiko;

    public ReservesIiko(ITablesService tablesService, IIikoService aikoService, IReservesService reservesService, IClientService clientService, IOptions<ForIIKO> iiko)
    {
        _tablesService = tablesService;
        _aikoService = aikoService;
        _reservesService = reservesService;
        _clientService = clientService;
        _aiko = iiko.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        /*_logger.Log(LogLevel.Information, "WORK");*/
        await _tablesService.Delete();
        await _reservesService.Delete();
        await _clientService.Delete();

        var newTestTable = new TableEntity
        {
            Id = Guid.Parse("2ff897b2-0d60-4ec9-913c-f95bae9a41ec"),
            Hall = "Зал",
            Number = 1,
            IsReserve = false,
            IsDeleted = false,
        };
        await _tablesService.Create(newTestTable);
        var newTestTable2 = new TableEntity
        {
            Id = Guid.Parse("dac8423f-47b7-4a41-84fc-63240f1c3b12"),
            Hall = "Лаунж",
            Number = 1,
            IsReserve = false,
            IsDeleted = false,
        };
        await _tablesService.Create(newTestTable2);
        /*var token = await _aikoService.GetToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        //create tables
        var modelForTables = new
        {
            terminalGroupIds = _aiko.terminalGroupId,
            returnSchema = false,
            revision = 0
        };
        var requestTables = JsonConvert.SerializeObject(modelForTables);
        var responseTables =
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/available_restaurant_sections",
                requestTables);
        var tables = await responseTables.Content.ReadAsStringAsync();
        var tableAllModel = JsonConvert.DeserializeObject<SectionDeserialize>(tables);
        var listTable = new List<TableEntity>();
        var sectionTableHall = tableAllModel.RestaurantSections.Where(a => a.Name.Equals("Зал"));
        var sectionTableLaung = tableAllModel.RestaurantSections.Where(a => a.Name.Equals("Лаунж"));

        foreach (var tdb in sectionTableHall)
        {
            listTable.AddRange(tdb.Tables.Select(newTable => new TableEntity()
            {
                Id = Guid.Parse(newTable.id),
                Hall = tdb.Name,
                Number = newTable.number,
                IsReserve = false,
                IsDeleted = false,
            }));
        }

        foreach (var tdb in sectionTableLaung)
        {
            listTable.AddRange(tdb.Tables.Select(newTable => new TableEntity()
            {
                Id = Guid.Parse(newTable.id),
                Hall = tdb.Name,
                Number = newTable.number,
                IsReserve = false,
                IsDeleted = false,
            }));
        }

        await _tablesService.Create(listTable);


        //create reserve
        var modelUpdateReserves = new
        {
            _aiko.organizationId,
            _aiko.terminalGroupId,
            _aiko.tableIds
        };
        var requestSection = JsonConvert.SerializeObject(modelUpdateReserves);
        await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/order/init_by_table",
            requestSection);
        var modelReserves = new
        {
            _aiko.restaurantSectionIds,
            dateFrom = $"{DateTime.Now:yyyy-MM-dd} 00:00:00.123"
        };

        var requestReserve = JsonConvert.SerializeObject(modelReserves);
        var responseReserve =
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/restaurant_sections_workload",
                requestReserve);

        var reserves = await responseReserve.Content.ReadAsStringAsync();
        var reserveModel = JsonConvert.DeserializeObject<ReservesDeserialize.Root>(reserves);
        var newReserve = new ReserveEntity();
        if (reserveModel.reserves != null)
        {
            foreach (var reserve in reserveModel.reserves)
            {
                newReserve.Id = reserve.Id;
                newReserve.DurationInMinutes = reserve.DurationInMinutes;
                newReserve.EstimatedStartTime = reserve.EstimatedStartTime;
                newReserve.GuestsCount = reserve.GuestsCount;
                await _reservesService.Create(newReserve);
            }
        }*/
    }
}