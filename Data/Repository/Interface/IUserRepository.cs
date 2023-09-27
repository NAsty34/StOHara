using Data.Model;

namespace Data.Repository.Interface;

public interface IUserRepository:  IBaseRepository<UserEntity>
{
    UserEntity? GetUser(string email);
}