using Data.Model;
using Data.Model.Entities;
using MaxOHara.Dto.BookingDto;

namespace MaxOHara.Dto;

public class TablesDto
{
    public TablesDto()
    {
    }

    public TablesDto(TablesEntity table)
    {
        Id = table.Id;
        Hall = table.Hall;
        Number = table.Number;
        IsReserve = table.IsReserve;
        if (table.Reserve != null) Reserve = new ReserveDto(table.Reserve);
        IsDeleted = table.IsDeleted;
    }
    public Guid Id { get; set; }
    public int Number { get; set; }
    public string Hall { get; set; }
    public bool IsReserve { get; set; }
    public ReserveDto? Reserve { get; set; }
    public bool IsDeleted { get; set; }
}