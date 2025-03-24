namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Responses;

public sealed class Response
{
    public object? Body { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public string Message { get; set; } = String.Empty;
}
