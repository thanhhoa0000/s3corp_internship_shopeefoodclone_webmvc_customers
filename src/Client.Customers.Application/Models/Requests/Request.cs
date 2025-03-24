namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class Request
{
    public required ApiMethod ApiMethod { get; set; } = ApiMethod.Get;
    public required string Url { get; set; }
    public object? Body { get; set; }
    public string? AccessToken { get; set; }
    public ResponseContentType ResponseContentType { get; set; } = ResponseContentType.Json;
}
