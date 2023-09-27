using MaxOHara.Dto.Lending;

namespace MaxOHara.Dto;

public class EditLendingDto
{
    public EditLendingDto()
    {
        
    }
    public EditLendingDto(BannerLendingDto bannerDto, List<AboutLendingDto> aboutDto, List<AtmosphereLendingDto> atmosphereDto)
    {
        BannerDto = bannerDto;
        AboutDto = aboutDto;
        AtmosphereDto = atmosphereDto;
    }
    public BannerLendingDto BannerDto { get; set; }
    public List<AboutLendingDto> AboutDto { get; set; }
    public List<AtmosphereLendingDto> AtmosphereDto { get; set; }
}