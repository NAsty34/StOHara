using System.ComponentModel;

namespace Data.Model.Entities;

public class MenuEntity:BaseEntity
{
    public Guid IdFile { get; set; }
    [DefaultValue(0)]
    public int Position { get; set; }
    public bool BusinessLunches { get; set; }
}
