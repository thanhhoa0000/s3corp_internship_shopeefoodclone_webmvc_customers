namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Responses;

public sealed class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
