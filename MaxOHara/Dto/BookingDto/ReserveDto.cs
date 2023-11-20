using Data.Model.Entities;

namespace MaxOHara.Dto.BookingDto;

public class ReserveDto
{
    public ReserveDto()
    {
    }

    public ReserveDto(ReserveEntity reserves)
    {
        Id = reserves.Id;
        if (reserves.Tables != null)
        {
            foreach (var table in reserves.Tables)
            {
                TableHallAndNumberList.Add($"{table.Hall}" + " " + $"{table.Number}"+" ");
            }
        }
        TableHallAndNumber = string.Join(string.Empty, TableHallAndNumberList.ToArray());
        EstimatedStartTime = reserves.EstimatedStartTime;
        DurationInMinutes = reserves.DurationInMinutes;
        GuestsCount = reserves.GuestsCount;
        Status = reserves.Status.ToString();
        if (reserves.Client != null) ClientId = reserves.Client.Id;
    }

    public Guid Id { get; set; }
    public string TableHallAndNumber { get; set; }
    private List<string> TableHallAndNumberList { get; set; } = new ();
    public DateTime EstimatedStartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int GuestsCount { get; set; }
    public string Status { get; set; }
    public Guid? ClientId { get; set; }
}