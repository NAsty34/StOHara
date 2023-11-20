using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Model.Entities;

public class ReserveEntity
{
    public Guid Id { get; set; }
    public StatusEntity Status { get; set; }
    public Guid? PaymentId { get; set; }
    public decimal? Price { get; set; }
    public DateTime EstimatedStartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int GuestsCount { get; set; }
    
    public Guid? ClientId { get; set; }
    [ForeignKey(nameof(ClientId))]
    public virtual ClientEnity? Client { get; set; }
    public DateTime CreatorDate { get; set; }
    
    public virtual List<TableEntity>? Tables { get; set; }
}