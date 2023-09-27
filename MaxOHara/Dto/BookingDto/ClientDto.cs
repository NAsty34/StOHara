using Data.Model.Entities;

namespace MaxOHara.Dto.BookingDto;

public class ClientDto
{
    public ClientDto()
    {
        
    }
    public ClientDto(ClientEnity client)
    {
        Id = client.Id;
        Name = client.Name;
        Surname = client.Name;
        Patronymic = client.Patronymic;
        Phone = client.Phone;
        Message = client.Message;
        Email = client.Email;
    }
    public Guid Id { get; set; }
    public string Name { get; set; } 
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string? Phone { get; set; }
    public string Email { get; set; }
    public string? Message { get; set; }
}