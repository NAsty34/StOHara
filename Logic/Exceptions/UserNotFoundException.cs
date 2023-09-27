using System.Net;

namespace Logic.Exceptions;

public class UserNotFoundException:BaseException
{
    public UserNotFoundException() : base("Пользователь не найден", HttpStatusCode.NotFound)
    {
    }
}