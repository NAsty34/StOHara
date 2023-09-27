namespace Data.Model.Json;

public class TerminalGroupsDeserialize
{
    public class Item
    {
        public string id { get; set; }
        public string organizationId { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string timeZone { get; set; }
    }

    public class Root
    {
        public string correlationId { get; set; }
        public List<TerminalGroup> terminalGroups { get; set; }
        public List<object> terminalGroupsInSleep { get; set; }
    }

    public class TerminalGroup
    {
        public string organizationId { get; set; }
        public List<Item> items { get; set; }
    }


}