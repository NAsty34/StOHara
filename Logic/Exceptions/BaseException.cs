
using System.Net;

namespace Logic.Exceptions;

public class BaseException:Exception
{
    public HttpStatusCode Status { get; set; }
    
    
    public BaseException(string msg, HttpStatusCode httpStatusCode) : base(msg)
    {
        Status = httpStatusCode;
    }
}