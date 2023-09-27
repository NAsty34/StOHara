using Data.Model;
using Data.Repository.Interface;
using Logic.Exceptions;
using Logic.Service.Interface;
using MaxOHara.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MaxOHara.Controllers;
[Authorize]
public class UserController:Controller
{
    private readonly IBaseService<UserEntity> _userServer;
    private readonly IScopeInfo _scope;
    private readonly IUserRepository _userRepository;

    public UserController(IBaseService<UserEntity> userServer, IScopeInfo scope, IUserRepository userRepository)
    {
        _userServer = userServer;
        _scope = scope;
        _userRepository = userRepository;
    }
    
    [Route("/api/v1/users/me")]
    [HttpGet]
    public async Task<ResponseDto<UserDto>> GetUser()
    {
        var user = _userServer.GetById(_scope.Userid);
        return new ResponseDto<UserDto>(new UserDto(user));
    }
    
    [Route("/api/v1/users")]
    [HttpGet]
    public async Task<ResponseDto<PageModel<UserDto>>> GetUsers(int? page, int? size)
    {
        var users = await _userServer.GetPage(page, size);
        var pageUser = PageModel<UserDto>.Create(users, users.Items!.Select(a => new UserDto(a)));
        return new ResponseDto<PageModel<UserDto>>(pageUser);
    }
    
    [Authorize (Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/user")]
    [HttpPost]
    public async Task<ResponseDto<UserDto>> CreateUser([FromBody] CreateUserDto userDto)
    {
        var newUser = new UserEntity()
        {
            Id = Guid.NewGuid(),
            Name = userDto.Name,
            Surname = userDto.Surname,
            Patronymic = userDto.Patronymic,
            Email = userDto.Email,
            Phone = userDto.PhoneNumber,
            HashPassword = BCrypt.Net.BCrypt.HashPassword(userDto.Password),
            Password = userDto.Password,
            RoleEntity = Enum.Parse<RoleEntity>(userDto.RoleEntity)
        };

        await _userRepository.Create(newUser);
        return new(new UserDto(newUser));
    }
    
    [Authorize (Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/user/{userId}")]
    [HttpPut]
    public async Task<ResponseDto<UserDto>> EditUser([FromBody]UserDto userEditDto, Guid userId)
    {
        var fromDb = _userServer.GetById(userId);
        if (fromDb == null) throw new UserNotFoundException();
        fromDb.Id = fromDb.Id;
        fromDb.Name = userEditDto.Name;
        fromDb.Surname = userEditDto.Surname;
        fromDb.Patronymic = userEditDto.Patronymic;
        fromDb.Email = userEditDto.Email;
        fromDb.Phone = userEditDto.PhoneNumber;
        fromDb.HashPassword = BCrypt.Net.BCrypt.HashPassword(userEditDto.Password);
        fromDb.Password = userEditDto.Password;
        fromDb.RoleEntity = Enum.Parse<RoleEntity>(userEditDto.RoleEntity);
        await _userRepository.Edit(fromDb);
        return new(new UserDto(fromDb));
    }
    
    [Authorize (Roles = nameof(RoleEntity.Admin))]
    [Route("/api/v1/user/{userId}")]
    [HttpDelete]
    public async Task<IActionResult> DeleteUser(Guid userId)
    {
        await _userRepository.Delete(userId);
        return Ok();
    }
}