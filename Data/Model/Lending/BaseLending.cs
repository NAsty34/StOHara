
using System.ComponentModel;

namespace Data.Model.Lending;

public class BaseLending
{
    public Guid Id { get; set; }
    public string? Header { get; set; }
    public Guid IdFile { get; set; }
}