namespace MaxOHara.Dto;

public class CreateNewsDto:NewsDto
{
    public List<Guid>? IdFile { get; set; }
}