using Data.Model;

namespace MaxOHara.Dto;

public class GalleryDto
{
    public GalleryDto()
    {
        
    }
    public GalleryDto(GalleryEntity? fileEntity)
    {
        Id = fileEntity.Id;
    }
    public Guid Id { get; set; }
    
    public string? File { get; set; }
}