using Logic.Service.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;

public class AuthController:Controller
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }
    
    [Route("/api/v1/auth/login")]
    [HttpPost]
    public async Task<ResponseDto<string>> Login([FromBody]LoginDto loginDto)
    {
        var tokenJwt = await _authService.Login(loginDto.Email, loginDto.Password );
        return new (tokenJwt);
    }

}