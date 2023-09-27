using System.Text.Json.Serialization;
using Data.Model.Lending;

namespace MaxOHara.Dto.Lending;

public class AboutLendingDto
{
    public AboutLendingDto()
    {
    }

    public AboutLendingDto(AboutLendingEntity aboutLendingEntity)
    {
        Id = aboutLendingEntity.Id;
        Header = aboutLendingEntity.Header;
        Description = aboutLendingEntity.Description;
        IdFile = aboutLendingEntity.IdFile;
        IsLeftPosition = aboutLendingEntity.IsLeftPosition;
    }

    public Guid Id { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public bool IsLeftPosition { get; set; }
    public Guid IdFile { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? UrlFile { get; set; }
}