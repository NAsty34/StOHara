using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class MarkElementdto
{
    public MarkElementdto()
    {
    }
    public MarkElementdto(SectionDeserialize.MarkElement markElement)
    {
        Text = markElement.text;
        Font = new FontDto(markElement.font);
        Color = new ColorDto(markElement.color);
        X = markElement.x;
        Y = markElement.y;
        Z = markElement.z;
        Angle = markElement.angle;
        Width = markElement.width;
        Height = markElement.height;
    }

    public string Text { get; set; }
    public FontDto Font { get; set; }
    public ColorDto Color { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Z { get; set; }
    public double Angle { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}