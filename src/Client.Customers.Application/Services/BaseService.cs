namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class BaseService : IBaseService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenProcessor _tokenProcessor;
    private readonly NLog.ILogger _logger;

    public BaseService(IHttpClientFactory httpClientFactory, ITokenProcessor tokenProcessor)
    {
        _httpClientFactory = httpClientFactory;
        _tokenProcessor = tokenProcessor;
        _logger = LogManager.GetCurrentClassLogger();
    }

    public async Task<Response?> SendAsync(Request request, bool bearer = true)
    {
        try
        {
            HttpClient client = _httpClientFactory.CreateClient("ShopeeFoodClone");
            
            _tokenProcessor.Client = client;
            _tokenProcessor.Logger = _logger;
            
            HttpRequestMessage message = new HttpRequestMessage();

            message.Headers.Add("Accept", "application/json");

            if (bearer)
            {
                var accessToken = _tokenProcessor.GetAccessToken();
                var refreshToken = _tokenProcessor.GetRefreshToken();
                var checkedAccessToken = 
                    await _tokenProcessor.GetValidAccessTokenAsync(accessToken!, refreshToken!);

                if (!string.IsNullOrEmpty(checkedAccessToken))
                {
                    message.Headers.Add("Authorization", $"Bearer {checkedAccessToken}");
                }
            }

            message.RequestUri = new Uri(request.Url);

            if (request.Body is not null)
                message.Content = new StringContent(JsonSerializer.Serialize(request.Body), Encoding.UTF8, "application/json");

            _logger.Debug($"\nRequest: {request.Body}");
            _logger.Debug($"\nRequest URL: {request.Url}");

            HttpResponseMessage? responseMessage = null;

            switch (request.ApiMethod)
            {
                case ApiMethod.Post:
                    message.Method = HttpMethod.Post;
                    break;
                case ApiMethod.Put:
                    message.Method = HttpMethod.Put;
                    break;
                case ApiMethod.Delete:
                    message.Method = HttpMethod.Delete;
                    break;
                default:
                    message.Method = HttpMethod.Get;
                    break;
            }

            responseMessage = await client.SendAsync(message);

            switch (responseMessage.StatusCode)
            {
                case HttpStatusCode.NotFound:
                    return new() { IsSuccessful = false, Message = "Not Found" };
                case HttpStatusCode.Forbidden:
                    return new() { IsSuccessful = false, Message = "Access Denied" };
                case HttpStatusCode.Unauthorized:
                    return new() { IsSuccessful = false, Message = "Unauthorized" };
                case HttpStatusCode.InternalServerError:
                    return new() { IsSuccessful = false, Message = "Internal Server Error" };
                default:
                    var content = await responseMessage.Content.ReadAsStringAsync();
                    _logger.Debug($"\nContent string: {content}");

                    var response = new Response();

                    if ((content.StartsWith("{") && content.EndsWith("}")) ||
                        (content.StartsWith("[") && content.EndsWith("]")))
                    {
                        response = JsonSerializer.Deserialize<Response>(
                            content,
                            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    }
                    else
                        response.Message = content.Trim();

                    return response;
            }
        }
        catch (Exception ex)
        {
            _logger.Error($"Error(s) occurred:\n-----{ex}");

            var response = new Response
            {
                Message = "Error(s) occurred!",
                IsSuccessful = false
            };

            return response;
        }
    }
}