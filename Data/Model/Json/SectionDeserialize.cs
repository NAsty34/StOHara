namespace Data.Model.Json;

public class SectionDeserialize
{
    public string CorrelationId { get; set; }
    public List<RestaurantSection> RestaurantSections { get; set; }
    public long Revision { get; set; }
 
    public class RestaurantSection
    {
        public string Id { get; set; }
        public string TerminalGroupId { get; set; }
        public string Name { get; set; }
        public List<Table> Tables { get; set; }
        public object? schema { get; set; }
    }
    
    public class Table
    {
        public string id { get; set; }
        public int number { get; set; }
        public string name { get; set; }
        public int seatingCapacity { get; set; }
        public object revision { get; set; }
        public bool isDeleted { get; set; }
        public string posId { get; set; }
    }
}