namespace Data.Model.Entities;

public class TableEntity
{
    public Guid Id { get; set; }
    public string? Hall { get; set; }
    public int Number { get; set; }
    public bool IsReserve { get; set; }
    public virtual List<ReserveEntity>? Reserves { get; set; }
    public bool IsDeleted { get; set; }
}