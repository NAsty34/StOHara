using System.Text.Json.Serialization;
using Data.Model.Lending;

namespace MaxOHara.Dto.Lending;

public class AtmosphereLendingDto
{
    public AtmosphereLendingDto()
    {
    }

    public AtmosphereLendingDto(AtmosphereLendingEntity atmosphereLendingEntity)
    {
        Id = atmosphereLendingEntity.Id;
        Header = atmosphereLendingEntity.Header;
        Description = atmosphereLendingEntity.Description;
        IsLeftPosition = atmosphereLendingEntity.IsLeftPosition;
        IdFile = atmosphereLendingEntity.IdFile;
    }

    public Guid Id { get; set; }
    public string Header { get; set; }
    public string Description { get; set; }
    public bool IsLeftPosition { get; set; }
    public Guid IdFile { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? UrlFile { get; set; }
}