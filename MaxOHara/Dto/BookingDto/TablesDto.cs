using Data.Model;
using Data.Model.Entities;
using MaxOHara.Dto.BookingDto;

namespace MaxOHara.Dto;

public class TablesDto
{
    public TablesDto()
    {
    }

    public TablesDto(TableEntity table)
    {
        Id = table.Id;
        Hall = table.Hall;
        Number = table.Number;
        IsReserve = table.IsReserve;
        if (table.Reserves.Count > 0) Reserve = new List<ReserveDto>(table.Reserves.Select(reserve=>new ReserveDto(reserve)));
        IsDeleted = table.IsDeleted;
    }
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string? Hall { get; set; }
    public bool IsReserve { get; set; }
    public List<ReserveDto> Reserve { get; set; }
    public bool IsDeleted { get; set; }
}