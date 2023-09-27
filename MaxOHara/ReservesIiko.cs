/*using System.Net.Http.Headers;
using Data.Model;
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
    private readonly ILogger<ReservesIiko> _logger;
    private readonly IClientService _clientService;
    private readonly ForIIKO _aiko;

    public ReservesIiko(ITablesService tablesService, IIikoService aikoService, IReservesService reservesService,
        ILogger<ReservesIiko> logger, IClientService clientService, IOptions<ForIIKO> iiko)
    {
        _tablesService = tablesService;
        _aikoService = aikoService;
        _reservesService = reservesService;
        _logger = logger;
        _clientService = clientService;
        _aiko = iiko.Value;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        _logger.Log(LogLevel.Information, "WORK");
        await _tablesService.Delete();
        await _reservesService.Delete();
        await _clientService.Delete();
        var token = await _aikoService.GetToken();
        Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

        var modelSection = new
        {
            terminalGroupIds = new List<string> { _aiko.terminalGroupId },
            returnSchema = true,
            revision = 0
        };
        var requestSection = JsonConvert.SerializeObject(modelSection);
        var responseSection =
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/available_restaurant_sections",
                requestSection);
        var result = await responseSection.Content.ReadAsStringAsync();
        var resultModel = JsonConvert.DeserializeObject<SectionDeserialize>(result);

        var removePersonalSection =
            resultModel.RestaurantSections.Where(a => a.Name != "Персонал" && a.Name != "Бар" && a.Name != "Веранда");

        var ids = new List<string>();
        ids.AddRange(removePersonalSection.Select(a => a.Id));
        var modelReserve = new
        {
            restaurantSectionIds = ids,
            dateFrom = $"{DateTime.Now:yyyy-MM-dd} 00:00:00.123"
        };
        var requestReserve = JsonConvert.SerializeObject(modelReserve);
        var responseReserve =
            await Client.PostAsJsonAsync("https://api-ru.iiko.services/api/1/reserve/restaurant_sections_workload",
                requestReserve);
        var reserves = await responseReserve.Content.ReadAsStringAsync();
        var reserveModel = JsonConvert.DeserializeObject<ReservesDeserialize.Root>(reserves);

        var listReserve = new List<ReservesEntity>();
        if (reserveModel.reserves != null)
        {
            listReserve.AddRange(reserveModel.reserves.Select(reserve => new ReservesEntity()
            {
                Id = reserve.Id,
                TableIds = reserve.TableIds,
                DurationInMinutes = reserve.DurationInMinutes,
                EstimatedStartTime = reserve.EstimatedStartTime,
                Status = StatusEntity.Success,
                GuestsCount = reserve.GuestsCount
            }));
            await _reservesService.Create(listReserve);
        }

        var hallChech = resultModel.RestaurantSections;

        foreach (var section in hallChech)
        {
            switch (section.Name)
            {
                /*case "Веранда":
                {
                    foreach (var sectionTable in section.Tables)
                    {
                        sectionTable.name = "Веранда";
                    }
                    break;
                }*/
     /*           case "Лаунж":
                {
                    foreach (var sectionTable in section.Tables)
                    {
                        sectionTable.name = "Лаунж";
                    }
                    break;
                }
                case "Зал":
                {
                    foreach (var sectionTable in section.Tables)
                    {
                        sectionTable.name = "Стол";
                    }
                    break;
                }
            }
        }

        var tables = removePersonalSection.Select(a => a.Tables);
        var listTablesDb = new List<TablesEntity>();
        foreach (var listTables in tables)
        {
            foreach (var table in listTables)
            {
                /*if (table is { name: "Веранда", number: > 4 }) continue;*/
/*                var tablesDb = new TablesEntity()
                {
                    Id = new Guid(table.id),
                    Hall = table.name,
                    Number = table.number,
                    IsReserve = false
                };
                var reserve = listReserve?.FirstOrDefault(a => a.TableIds.Contains(table.id));
                if (reserve != null)
                {
                    tablesDb.IsReserve = true;
                    tablesDb.Reserve = reserve;
                }

                listTablesDb.Add(tablesDb);
            }
        }

        await _tablesService.Create(listTablesDb);
    }
}*/