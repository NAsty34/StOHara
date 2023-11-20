using Data.Repository.Interface;

namespace MaxOHara.Middleware;

public class CheckBookingMiddleware
{
    private readonly RequestDelegate _next;
    public CheckBookingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IFeatureRepository featureRepository)
    {
        var pathValue = context.Request.Path.Value;
        if (pathValue != null && pathValue.Contains("booking"))
        {
            if (featureRepository.GetBool().IsCheck)
            {
                context.Response.StatusCode = 404;
                return;
            }
        }
        await _next(context);
    }
}