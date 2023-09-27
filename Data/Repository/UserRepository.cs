using Data.Model;
using Data.Repository.Interface;

namespace Data.Repository;

public class UserRepository:BaseRepository<UserEntity>, IUserRepository
{
    public UserRepository(MaxOHaraContext context, IScopeInfo scope) : base(context, scope)
    {
    }

    public UserEntity? GetUser(string email)
    {
        return DbSet.FirstOrDefault(u => u.Email == email);
    }
    
}