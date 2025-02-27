namespace ShopeeFoodClone.WebMvc.Customers.Interfaces;

public interface ITokenProcessor
{
    HttpClient Client { get; set; }
    NLog.ILogger Logger { get; set; }
        
    void SetTokens(string accessToken, string refreshToken);
    string? GetAccessToken();
    string? GetRefreshToken();
    Task<string?> GetValidAccessTokenAsync(string accessToken, string refreshToken);
    void ClearTokens();
}