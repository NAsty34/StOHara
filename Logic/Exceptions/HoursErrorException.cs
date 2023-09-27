using System.Net;

namespace Logic.Exceptions;

public class HoursErrorException:BaseException
{
    public HoursErrorException() : base("Забронировать столик на выбранное время невозможно", HttpStatusCode.BadRequest)
    {
    }
}