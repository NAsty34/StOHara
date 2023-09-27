using Data.Model.Entities;

namespace MaxOHara.Dto.BookingDto;

public class ReserveDto
{
    public ReserveDto()
    {
        
    }
    public ReserveDto(ReservesEntity reserve)
    {
        Id = reserve.Id;
        TableIds = reserve.TableIds;
        EstimatedStartTime = reserve.EstimatedStartTime;
        DurationInMinutes = reserve.DurationInMinutes;
        GuestsCount = reserve.GuestsCount;
        Status = reserve.Status.ToString();
        if (reserve.Client != null) ClientId = reserve.Client.Id;
    }
    public Guid Id { get; set; }
    public List<string>? TableIds { get; set; }
    public DateTime EstimatedStartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int GuestsCount { get; set; }
    public string Status { get; set; }
    public Guid? ClientId { get; set; }
}