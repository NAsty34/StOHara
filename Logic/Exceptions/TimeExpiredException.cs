using System.Net;

namespace Logic.Exceptions;

public class TimeExpiredException:BaseException
{
    public TimeExpiredException() : base("Время оплаты истекло", HttpStatusCode.NotFound)
    {
    }
}