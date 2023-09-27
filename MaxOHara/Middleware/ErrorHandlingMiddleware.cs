using System.Net;
using Logic.Exceptions;
using MaxOHara.Dto;

namespace MaxOHara.Middleware;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;
    

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, ILogger<ErrorHandlingMiddleware> logger)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            logger.Log(LogLevel.Information, "==================" + ex);
            await HandleException(context, ex);
        }
    }

    private static Task HandleException(HttpContext context, Exception ex)
    {
        
        var code = HttpStatusCode.InternalServerError;
        ResponseDto<string> response = new ResponseDto<string>(ex.ToString());
        /*var response = new ResponseDto<string>(ex.Message);
        if (ex is BaseException)
        {
            code = (ex as BaseException)!.Status;
        }*/
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)code;
        return context.Response.WriteAsJsonAsync(response);
    }
}