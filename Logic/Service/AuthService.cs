using System.IdentityModel.Tokens.Jwt;
using Data.Model;
using Data.Repository.Interface;
using Logic.Exceptions;
using Logic.Service.Interface;
using Microsoft.Extensions.Logging;

namespace Logic.Service;

public class AuthService:IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IJwtService _jwtService;

    public AuthService(IUserRepository userRepository, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _jwtService = jwtService;
    }

    public async Task<string> Login(string email, string password)
    {
        var user = _userRepository.GetUser(email);
        if (user == null) throw new UserNotFoundException();
        if (!BCrypt.Net.BCrypt.Verify(password, user.HashPassword)) 
            throw new PasswordIncorrectException();
        var jwt = await _jwtService.GenerateJwt(user.Id, user.RoleEntity.ToString());
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}