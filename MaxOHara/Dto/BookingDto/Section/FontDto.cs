using Data.Model.Json;

namespace MaxOHara.Dto.BookingDto;

public class FontDto
{
    public FontDto()
    {
        
    }
    public FontDto(SectionDeserialize.Font markElementFont)
    {
        FontFamily = markElementFont.fontFamily;
        Size = markElementFont.size;
        FontStyle = markElementFont.fontStyle;
    }

    public string FontFamily { get; set; }
    public double Size { get; set; }
    public string FontStyle { get; set; }
}