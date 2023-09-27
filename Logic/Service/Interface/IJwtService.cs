using System.IdentityModel.Tokens.Jwt;

namespace Logic.Service.Interface;

public interface IJwtService
{
    Task<JwtSecurityToken> GenerateJwt(Guid id, string role);
}