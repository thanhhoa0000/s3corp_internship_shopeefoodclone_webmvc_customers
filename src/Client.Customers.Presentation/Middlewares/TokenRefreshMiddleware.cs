namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Middlewares;

public class TokenRefreshMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IHttpClientFactory _clientFactory;

    public TokenRefreshMiddleware(RequestDelegate next, IHttpClientFactory clientFactory)
    {
        _next = next;
        _clientFactory = clientFactory;
    }

    public async Task InvokeAsync(HttpContext context, ITokenProcessor processor)
    {
        processor.Client = _clientFactory.CreateClient("ShopeeFoodClone");
        processor.Logger = LogManager.GetLogger(nameof(TokenRefreshMiddleware));

        var accessToken = processor.GetAccessToken();
        var refreshToken = processor.GetRefreshToken();

        if (string.IsNullOrEmpty(accessToken) || string.IsNullOrEmpty(refreshToken))
        {
            await _next(context);
            return;
        }

        var checkedTokens = await processor.GetValidAccessTokenAsync(accessToken, refreshToken);

        processor.SetTokens(checkedTokens!.AccessToken, checkedTokens.RefreshToken);

        await _next(context);
    }
}
