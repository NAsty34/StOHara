using Data.Model;

namespace MaxOHara.Dto;

public class UserDto
{
    public UserDto()
    {
        
    }
    public UserDto(UserEntity? user)
    {
        Id = user.Id;
        Name = user.Name;
        Surname = user.Surname;
        Patronymic = user.Patronymic;
        Email = user.Email;
        PhoneNumber = user.Phone;
        RoleEntity = user.RoleEntity.ToString();
        Password = user.Password;
    }
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Patronymic { get; set; }
    public string Password { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
    public string RoleEntity { get; set; } 
}