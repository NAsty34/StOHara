
namespace Data.Model.Entities;

public class ClientEnity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }   
}