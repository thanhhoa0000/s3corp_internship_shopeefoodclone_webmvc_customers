namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class TokenProcessor : ITokenProcessor
{
    private readonly IHttpContextAccessor _accessor;
    private HttpClient _client;
    private NLog.ILogger _logger;

    public HttpClient Client { get => _client; set => _client = value; }
    public NLog.ILogger Logger { get => _logger; set => _logger = value; }

    public TokenProcessor(IHttpContextAccessor accessor, HttpClient client)
    {
        _accessor = accessor;
        _client = client;
        _logger = LogManager.GetCurrentClassLogger();
    }

    public void ClearTokens()
    {
        _accessor.HttpContext?.Response.Cookies.Delete(CookieProperties.AccessTokenCookie);
        _accessor.HttpContext?.Response.Cookies.Delete(CookieProperties.RefreshTokenCookie);
    }
    
    public string? GetAccessToken()
    {
        string? token = null;
        bool? hasToken = _accessor
            .HttpContext?.Request.Cookies.TryGetValue(CookieProperties.AccessTokenCookie, out token);

        return hasToken is true ? token : null;
    }
    
    public string? GetRefreshToken()
    {
        string? token = null;
        bool? hasToken = _accessor
            .HttpContext?.Request.Cookies.TryGetValue(CookieProperties.RefreshTokenCookie, out token);

        return hasToken is true ? token : null;
    }

    public void SetTokens(string accessToken, string refreshToken)
    {
        _accessor.HttpContext?.Response.Cookies.Append(
            CookieProperties.AccessTokenCookie, accessToken);
        
        _accessor.HttpContext?.Response.Cookies.Append(
            CookieProperties.RefreshTokenCookie, refreshToken);
    }

    public async Task<string?> GetValidAccessTokenAsync(string accessToken, string refreshToken)
    {
        if (!IsTokenExpired(accessToken))
            return accessToken;

        return await RefreshAccessTokenAsync(refreshToken);
    }

    private async Task<string?> RefreshAccessTokenAsync(string refreshToken)
    {
        _logger.Debug("\n----\nRefreshing access token\n----\n");
        
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{ApiUrlProperties.ApiGatewayUrl}/refresh_token_login")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(new LoginRefreshTokenRequest(refreshToken)),
                Encoding.UTF8,
                "application/json")
        };
        
        var response = await _client.SendAsync(request);
        
        _logger.Debug(response.Content.ToString());
        
        if (!response.IsSuccessStatusCode)
        {
            _logger.Error("Failed to refresh token. Status Code: {StatusCode}", response.StatusCode);
            return null;
        }
        
        var content = await response.Content.ReadAsStringAsync();
        
        _logger.Debug(content);
        
        var tokenResponse = new Response();

        tokenResponse = JsonSerializer.Deserialize<Response>(
            content,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        LoginResponse loginResponse
            = JsonSerializer.Deserialize<LoginResponse>(
                Convert.ToString(tokenResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        if (loginResponse == null || string.IsNullOrEmpty(loginResponse.AccessToken))
        {
            _logger.Error("Invalid response from token refresh.");
            return null;
        }
        
        SetTokens(loginResponse.AccessToken, loginResponse.RefreshToken);
        
        return loginResponse.AccessToken;
    }
    
    private static bool IsTokenExpired(string? token)
    {
        if (string.IsNullOrEmpty(token))
            return true;
        
        var handler = new JwtSecurityTokenHandler();
        var jwtToken = handler.ReadJwtToken(token);
        
        return jwtToken.ValidTo < DateTime.UtcNow;
    }
}
    