using System.Net;

namespace Logic.Exceptions;

public class PastDateException:BaseException
{
    public PastDateException() : base("Вы не можешь выбрать прошедшее время", HttpStatusCode.BadRequest)
    {
    }
}