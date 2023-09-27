namespace Data.Model.Entities;

public class ReservesEntity
{
    public Guid Id { get; set; }
    public StatusEntity Status { get; set; }
    public Guid? PaymentId { get; set; }
    public decimal? Price { get; set; }
    public List<string> TableIds { get; set; }
    public DateTime EstimatedStartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int GuestsCount { get; set; }
    public virtual ClientEnity? Client { get; set; }
    public DateTime CreatorDate { get; set; }
}