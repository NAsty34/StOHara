using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class TableElementDto
{
    public TableElementDto()
    {
        
    }
    public TableElementDto(SectionDeserialize.TableElement tableElement)
    {
        TableId = tableElement.tableId;
        X = tableElement.x;
        Y = tableElement.y;
        Z = tableElement.z;
        Angle = tableElement.angle;
        Width = tableElement.width;
        Height = tableElement.height;
    }

    public string TableId { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public double Angle { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}