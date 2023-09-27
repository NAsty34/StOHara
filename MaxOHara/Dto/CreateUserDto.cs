
namespace MaxOHara.Dto;

public class CreateUserDto:UserDto
{
    public string Password { get; set; } = null!;
}