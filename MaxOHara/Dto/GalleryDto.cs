using Data.Model;
using Data.Model.Entities;

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