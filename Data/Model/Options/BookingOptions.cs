namespace Data.Model.Options;

public class BookingOptions
{
    public const string Booking = "Booking";
    public string ShopId { get; set; }
    public string SecretKey { get; set; }
    public string ReturnUrlForPayment { get; set; }
    public string ReturnUrl { get; set; }
    public string Currency { get; set; }
    public TimeOnly Duration { get; set; }
}