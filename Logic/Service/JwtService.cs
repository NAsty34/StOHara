using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Data.Model.Options;
using Logic.Service.Interface;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Logic.Service;

public class JwtService : IJwtService
{
    private readonly JwtTokenOptions _options;

    public JwtService(IOptions<JwtTokenOptions> options)
    {
        _options = options.Value;
    }

    public async Task<JwtSecurityToken> GenerateJwt(Guid id, string role)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Role, role),
            new(ClaimTypes.Actor, id.ToString())
        };

        var jwt = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_options.Time)),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.Key)),
                SecurityAlgorithms.HmacSha256));

        return jwt;
    }
}