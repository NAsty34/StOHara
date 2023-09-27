using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class SchemaDto
{
    public SchemaDto()
    {
    }

    public SchemaDto(SectionDeserialize.Schema sectionSchema)
    {
        Width = sectionSchema.width;
        Height = sectionSchema.height;
        MarkElements = new List<MarkElementdto>(sectionSchema.markElements.Select(a => new MarkElementdto(a)));
        TableElements = new List<TableElementDto>(sectionSchema.tableElements.Select(a=>new TableElementDto(a)));
        RectangleElements = new List<RectangleElementDto>(sectionSchema.rectangleElements.Select(a=>new RectangleElementDto(a)));
        EllipseElements = new List<object>(sectionSchema.ellipseElements);
        Revision = sectionSchema.revision;
        IsDeleted = sectionSchema.isDeleted;
    }

    public int Width { get; set; }
    public int Height { get; set; }
    public List<MarkElementdto> MarkElements { get; set; }
    public List<TableElementDto> TableElements { get; set; }
    public List<RectangleElementDto> RectangleElements { get; set; }
    public List<object> EllipseElements { get; set; }
    public object Revision { get; set; }
    public bool IsDeleted { get; set; }
}