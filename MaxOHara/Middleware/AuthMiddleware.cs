using System.Security.Claims;
using Data.Model;
using Data.Repository.Interface;
using Logic.Service.Interface;
using Microsoft.AspNetCore.Authorization;

namespace MaxOHara.Middleware;
[Authorize]
public class AuthMiddleware
{
    private readonly RequestDelegate _next;
    
    public AuthMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context, IScopeInfo scope, IBaseService<UserEntity> userServer)
    {
        
        if (context.User.Identity is { IsAuthenticated: true })
        {
            
            scope.Userid = Guid.Parse(context.User.Claims.First(a => a.Type == ClaimTypes.Actor).Value);
            
            var user = userServer.GetById(scope.Userid);
            scope.Role = user.RoleEntity;
        }

        await _next(context);
    }
}