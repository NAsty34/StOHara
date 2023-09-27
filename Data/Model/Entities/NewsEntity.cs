using System.ComponentModel.DataAnnotations;

namespace Data.Model;

public class NewsEntity:BaseEntity
{
    public List<Guid>? IdFile { get; set; }
    [Required(ErrorMessage = "Укажите загаловок новости")]
    public string Header { get; set; }
    [Required(ErrorMessage = "Укажите содержание новости")]
    public string Description { get; set; }
}