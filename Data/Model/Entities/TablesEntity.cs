using Data.Model.Entities;

namespace Data.Model;

public class TablesEntity
{
    public Guid Id { get; set; }
    public string Hall { get; set; }
    public int Number { get; set; }
    public bool IsReserve { get; set; }
    public virtual ReservesEntity? Reserve { get; set; }
    public bool IsDeleted { get; set; }
}