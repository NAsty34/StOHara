
namespace Data.Model;

public class BaseEntity
{
    public Guid Id { get; set; }
    public Guid? IdUser { get; set; }
    public DateTime CreatorDate { get; set; }
}