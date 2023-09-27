using System.Text.Json.Serialization;
using Data.Model.Entities;
using Yandex.Checkout.V3;

namespace MaxOHara.Dto;

public class PaymentDto
{
    public PaymentDto()
    {
        
    }
    public PaymentDto(ClientEnity clientEntity)
    {
        
    }
    
    public Guid Id { get; set; }
    public bool Paid { get; set; }
    public string Status { get; set; }
    public bool? Test { get; set; }
    public string ConfirmationUrl { get; set; }
}