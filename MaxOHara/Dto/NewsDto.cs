using Data.Model;

namespace MaxOHara.Dto;

public class NewsDto
{
    public NewsDto()
    {
        
    }
    public NewsDto(NewsEntity? newsEntity)
    {
        Id = newsEntity.Id;
        Header = newsEntity.Header;
        Description = newsEntity.Description;
        IdFile = newsEntity.IdFile;
        CreateData = newsEntity.CreatorDate.ToShortDateString();
    }
    public Guid Id { get; set; }
    public string Header { get; set; } 
    public string Description { get; set; }
    public List<Guid> IdFile { get; set; }
    public List<string>? File { get; set; }
    public string CreateData { get; set; }
}