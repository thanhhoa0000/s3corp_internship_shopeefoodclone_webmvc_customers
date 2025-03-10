namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class Response
{
    public object? Body { get; set; }
    public bool IsSuccessful { get; set; } = true;
    public string Message { get; set; } = String.Empty;
}