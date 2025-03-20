namespace ShopeeFoodClone.WebMvc.Customers.Application.Responses;

public sealed class LoginResponse
{
    public required string AccessToken { get; set; }
    public required string RefreshToken { get; set; }
}
