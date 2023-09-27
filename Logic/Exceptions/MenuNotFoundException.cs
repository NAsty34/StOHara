using System.Net;

namespace Logic.Exceptions;

public class MenuNotFoundException:BaseException
{
    public MenuNotFoundException() : base("Меню не найдено", HttpStatusCode.NotFound)
    {
    }
}