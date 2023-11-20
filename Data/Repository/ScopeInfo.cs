using Data.Repository.Interface;

namespace Data.Repository;

public class ScopeInfo:IScopeInfo
{
    public Guid Userid { get; set; } 
    public Enum Role { get; set; } = null!;
}