namespace Data.Model.Options;

public class AdminForEmail
{
    public const string Admin = "Admin";
    public string AdminEmail { get; set; } = String.Empty;
    public string EmailToken { get; set; } = String.Empty;
    public string NameSender { get; set; } = String.Empty;
}