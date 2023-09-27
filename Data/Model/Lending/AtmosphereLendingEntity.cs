using System.ComponentModel;

namespace Data.Model.Lending;

public class AtmosphereLendingEntity:BaseLending
{
    public string Description { get; set; }
    public bool IsLeftPosition { get; set; }
}