using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class ColorDto
{
    public ColorDto()
    {
        
    }
    public ColorDto(SectionDeserialize.Color markElementColor)
    {
        A = markElementColor.a;
        R = markElementColor.r;
        G = markElementColor.g;
        B = markElementColor.b;
    }

    public int A { get; set; }
    public int R { get; set; }
    public int G { get; set; }
    public int B { get; set; }
}