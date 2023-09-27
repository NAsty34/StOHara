using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class RestaurantSectionDto
{
    public RestaurantSectionDto()
    {
    }

    public RestaurantSectionDto(SectionDeserialize.RestaurantSection section)
    {
        Id = section.Id;
        TerminalGroupId = section.TerminalGroupId;
        Name = section.Name;
        Tables = new List<TableDto>(section.Tables.Select(a=>new TableDto(a)));
        if (section.schema != null)
        {
            Schema = new SchemaDto(section.schema);   
        }
    }

    public string Id { get; set; }
    public string TerminalGroupId { get; set; }
    public string Name { get; set; }
    public List<TableDto> Tables { get; set; }
    public SchemaDto? Schema { get; set; }
}