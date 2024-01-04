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

    public ReservesIiko(ITablesService tablesService, IIikoService aikoService, IReservesService reservesService,
        IClientService clientService, IOptions<ForIIKO> iiko)
    {
        _tablesService = tablesService;
        _aikoService = aikoService;
        _reservesService = reservesService;
        _clientService = clientService;
        _aiko = iiko.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        await _tablesService.Delete();
        await _reservesService.Delete();
        await _clientService.Delete();

        var token = await _aikoService.GetToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


        //create tables
        var modelForTables = new
        {
            _aiko.restaurantSectionIds,
            dateFrom = $"{DateTime.Now:yyyy-MM-dd} 00:00:00.123",
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

        var sectionTableHall = tableAllModel.RestaurantSections.Where(a => a.Equals("Стол"));
        var listTableLaunge = new List<TableEntity>();
        var listTableHall = new List<TableEntity>();


        foreach (var tdb in tableAllModel.RestaurantSections)
        {
            listTableHall.AddRange(tdb.Tables.Where(a => a.name.Contains("Стол")).Select(newTable => new TableEntity()
            {
                Id = Guid.Parse(newTable.id),
                Hall = newTable.name,
                Number = newTable.number,
                IsReserve = false,
                IsDeleted = false
            }));
        }

        foreach (var tdb in tableAllModel.RestaurantSections.Where(a => a.Name.Equals("Лаунж")))
        {
            listTableLaunge.AddRange(tdb.Tables.Select(newTable => new TableEntity()
            {
                Id = Guid.Parse(newTable.id),
                Hall = tdb.Name,
                Number = newTable.number,
                IsReserve = false,
                IsDeleted = false,
            }));
        }

        await _tablesService.Create(listTableHall);
        await _tablesService.Create(listTableLaunge);

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
        if (reserveModel.reserves != null)
        {
            var listReserve = new List<ReserveEntity>();
            listReserve.AddRange(reserveModel.reserves.Select(reserf => new ReserveEntity()
            {
                Id = reserf.Id,
                DurationInMinutes = reserf.DurationInMinutes,
                EstimatedStartTime = reserf.EstimatedStartTime,
                GuestsCount = reserf.GuestsCount,
                Tables = _tablesService.GetByIds(reserf.TableIds),
            }));
            await _reservesService.Create(listReserve);
        }
    }
}