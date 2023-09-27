using System.Net;

namespace Logic.Exceptions;

public class PasswordIncorrectException:BaseException
{
    public PasswordIncorrectException() : base("Password incorrect", HttpStatusCode.BadRequest)
    {
    }
}