using System.Text.Json.Serialization;
using Data.Model;

namespace MaxOHara.Dto;

public class MenuDto
{
    public MenuDto()
    {
        
    }
    public MenuDto(MenuEntity menuEntity)
    {
        Id = menuEntity.Id;
        IdFile = menuEntity.IdFile;
        Position = menuEntity.Position;
        Launch = menuEntity.BusinessLunches;
    }
    
    public Guid Id { get; set; }
    public Guid IdFile { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string File { get; set; }
    public int Position { get; set; }
    public bool Launch { get; set; }
}