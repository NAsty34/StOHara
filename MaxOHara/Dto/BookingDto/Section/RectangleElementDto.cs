using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class RectangleElementDto
{
    public RectangleElementDto()
    {
    }
    public RectangleElementDto(SectionDeserialize.RectangleElement rectangleElement)
    {
        Color = new ColorDto(rectangleElement.color);
        X = rectangleElement.x;
        Y = rectangleElement.y;
        Z = rectangleElement.z;
        Angle = rectangleElement.angle;
        Width = rectangleElement.width;
        Height = rectangleElement.height;
    }

    public ColorDto Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public double Angle { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}