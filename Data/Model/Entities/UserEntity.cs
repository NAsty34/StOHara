using System.ComponentModel.DataAnnotations;

namespace Data.Model.Entities;

public class UserEntity:BaseEntity
{
    
    [Required(ErrorMessage = "Укажите имя")]
    public string? Name { get; set; }
    [Required(ErrorMessage = "Укажите фамилию")]
    public string? Surname { get; set; }
    [Required(ErrorMessage = "Укажите отчество")]
    public string? Patronymic { get; set; }
    [Required(ErrorMessage = "Укажите email")]
    [EmailAddress]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Укажите номер телефона")]
    [Phone]
    public string Phone { get; set; } = null!;

    [Required(ErrorMessage = "Укажите роль")]
    public RoleEntity RoleEntity { get; set; }
    [Required(ErrorMessage = "Укажите пароль")]
    public string HashPassword { get; set; } = null!;
    public string Password { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}