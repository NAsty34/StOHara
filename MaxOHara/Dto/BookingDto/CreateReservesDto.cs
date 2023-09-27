
using Data.Model.Entities;

namespace MaxOHara.Dto.BookingDto;

public class CreateReservesDto
{
    public string Name { get; set; } 
    public string Surname { get; set; }
    public string? Patronymic { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Message { get; set; }
    public List<string> TableIds { get; set; }
    public DateTime EstimatedStartTime { get; set; }
    public int DurationInMinutes { get; set; }
    public int GuestsCount { get; set; }
}