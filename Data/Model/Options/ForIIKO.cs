namespace Data.Model.Options;

public class ForIIKO
{
    public const string DataShopForIiko = "DataShopForIIKO";
    public string Login { get; set; }
    public string organizationId { get; set; }
    
    public List<string> terminalGroupId { get; set; }
    public List<string> tableIds { get; set; }
    public List<string> restaurantSectionIds { get; set; }
}