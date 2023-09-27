using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class RootSectionDto
{
    public RootSectionDto()
    {
    }

    public RootSectionDto(SectionDeserialize section)
    {
        CorrelationId = section.CorrelationId;
        RestaurantSections = new List<RestaurantSectionDto>(section.RestaurantSections.Select(a => new RestaurantSectionDto(a)));
        Revision = section.Revision;
    }

    public string CorrelationId { get; set; }
    public List<RestaurantSectionDto> RestaurantSections { get; set; }
    public long Revision { get; set; }
}