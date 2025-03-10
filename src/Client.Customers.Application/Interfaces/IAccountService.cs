namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IAccountService
{
    Task<Response?> LoginAsync(LoginViewModel model);
    Task<Response?> RegisterAsync(RegisterViewModel model);
    Task<Response?> LoginWithRefreshTokenAsync(LoginRefreshTokenRequest model);
}