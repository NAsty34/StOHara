using Data.Model;

namespace MaxOHara.Dto;

public class FilesDto
{
    public FilesDto()
    {
        
    }
    public FilesDto(FileEntity fileEntity)
    {
        Id = fileEntity.Id;
    }
    public Guid Id { get; set; }
    
    public string File { get; set; }
}