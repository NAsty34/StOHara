using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class TableDto
{
    public TableDto()
    {
        
    }

    public TableDto(SectionDeserialize.Table table)
    {
        Id = table.id;
        Number = table.number;
        Name = table.name;
        SeatingCapacity = table.seatingCapacity;
        Revision = table.revision;
        IsDeleted = table.isDeleted;
    }

    public string Id { get; set; }
    public int Number { get; set; }
    public string Name { get; set; }
    public int SeatingCapacity { get; set; }
    public object Revision { get; set; }
    public bool IsDeleted { get; set; }
}