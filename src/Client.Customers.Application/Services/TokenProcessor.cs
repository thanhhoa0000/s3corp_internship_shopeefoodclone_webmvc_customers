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
        if (_accessor.HttpContext is null)
        {
            _logger.Error("HttpContext is null");
        }

        var options = new CookieOptions
        {
            Secure = true,
            HttpOnly = false,
            IsEssential = true
        };
        
        _accessor.HttpContext?.Response.Cookies.Append(
            CookieProperties.AccessTokenCookie, accessToken, options);
        
        _accessor.HttpContext?.Response.Cookies.Append(
            CookieProperties.RefreshTokenCookie, refreshToken, options);
    }

    public async Task<LoginResponse?> GetValidAccessTokenAsync(string accessToken, string refreshToken)
    {
        if (!IsTokenExpired(accessToken))
            return new LoginResponse
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };

        return await RefreshAccessTokenAsync(refreshToken);
    }

    private async Task<LoginResponse?> RefreshAccessTokenAsync(string refreshToken)
    {
        _logger.Debug("\n----------\nRefreshing tokens\n----------");
        
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{ApiUrlProperties.ApiGatewayUrl}/refresh_token_login")
        {
            Content = new StringContent(
                JsonSerializer.Serialize(new LoginRefreshTokenRequest(refreshToken)),
                Encoding.UTF8,
                "application/json")
        };
        
        var response = await _client.SendAsync(request);
        
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
        
        _logger.Debug("\n----------\nTokens refreshed\n----------");

        return loginResponse;
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
    