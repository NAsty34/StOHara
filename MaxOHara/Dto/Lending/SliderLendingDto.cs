using System.Text.Json.Serialization;
using Data.Model.Lending;

namespace MaxOHara.Dto.Lending;

public class SliderLendingDto
{
    public SliderLendingDto()
    {
        
    }
    public SliderLendingDto(SliderLendingEntity sliderLendingEntity)
    {
        Id = sliderLendingEntity.Id;
        Header = sliderLendingEntity.Header;
        IdFile = sliderLendingEntity.IdFile;
    }
    public Guid Id { get; set; }
    public string Header { get; set; }
    public Guid IdFile { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? UrlFile { get; set; }
}