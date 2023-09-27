using Data.Model;
using Data.Repository.Interface;

namespace Logic.Service;

public class UserService:BaseService<UserEntity>
{
    public UserService(IUserRepository userRepository) : base(userRepository)
    {
        
    }
    
}