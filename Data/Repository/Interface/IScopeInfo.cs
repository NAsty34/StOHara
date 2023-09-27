
namespace Data.Repository.Interface;

public interface IScopeInfo
{
    public Guid Userid { get; set; }
    public Enum Role { get; set; }
}