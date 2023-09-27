using System.Net;

namespace Logic.Exceptions;

public class NewsNotFoundException:BaseException
{
    public NewsNotFoundException() : base("Новость не найдена не найден", HttpStatusCode.NotFound)
    {
    }
}