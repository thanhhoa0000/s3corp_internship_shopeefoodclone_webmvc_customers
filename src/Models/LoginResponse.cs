namespace ShopeeFoodClone.WebMvc.Customers.Models;

public class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
