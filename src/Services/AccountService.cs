namespace ShopeeFoodClone.WebMvc.Customers.Services;

public class AccountService : IAccountService
{
    private readonly IBaseService _service;

    public AccountService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> LoginAsync(LoginViewModel model)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = model,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/login"
        }, bearer: false);
    }
        
    public async Task<Response?> LoginWithRefreshTokenAsync(LoginRefreshTokenRequest model)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = model,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/refresh_token_login"
        }, bearer: false);
    }

    public async Task<Response?> RegisterAsync(RegisterViewModel model)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = model,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/register"
        }, bearer: false);
    }
}