using System.Text.Json.Serialization;
using Data.Model.Lending;

namespace MaxOHara.Dto.Lending;

public class BannerLendingDto
{
    public BannerLendingDto()
    {
        
    }
    public BannerLendingDto(BannerLendingEntity bannerLendingEntity)
    {
        Id = bannerLendingEntity.Id;
        Header = bannerLendingEntity.Header;
        IdFile = bannerLendingEntity.IdFile;
    }
    public Guid Id { get; set; }
    public string Header { get; set; }
    public Guid IdFile { get; set; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
    public string? UrlFile { get; set; }
}