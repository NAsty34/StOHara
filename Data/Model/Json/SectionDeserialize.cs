namespace Data.Model.Json;

public class SectionDeserialize
{
    public string CorrelationId { get; set; }
    public List<RestaurantSection> RestaurantSections { get; set; }
    public long Revision { get; set; }
    public class Color
    {
        public int a { get; set; }
        public int r { get; set; }
        public int g { get; set; }
        public int b { get; set; }
    }

    public class Font
    {
        public string fontFamily { get; set; }
        public double size { get; set; }
        public string fontStyle { get; set; }
    }

    public class MarkElement
    {
        public string text { get; set; }
        public Font font { get; set; }
        public Color color { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public double angle { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class RectangleElement
    {
        public Color color { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public double angle { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }

    public class RestaurantSection
    {
        public string Id { get; set; }
        public string TerminalGroupId { get; set; }
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public Schema? schema { get; set; }
    }
    
    public class Schema
    {
        public int width { get; set; }
        public int height { get; set; }
        public List<MarkElement> markElements { get; set; }
        public List<TableElement> tableElements { get; set; }
        public List<RectangleElement> rectangleElements { get; set; }
        public List<object> ellipseElements { get; set; }
        public object revision { get; set; }
        public bool isDeleted { get; set; }
    }

    public class Table
    {
        public string id { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public int seatingCapacity { get; set; }
        public object revision { get; set; }
        public bool isDeleted { get; set; }
    }

    public class TableElement
    {
        public string tableId { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int z { get; set; }
        public double angle { get; set; }
        public int width { get; set; }
        public int height { get; set; }
    }
}