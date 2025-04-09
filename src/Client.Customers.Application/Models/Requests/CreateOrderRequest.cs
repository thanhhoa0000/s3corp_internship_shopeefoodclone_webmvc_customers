namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class CreateOrderRequest
{
    public CartDto? Cart { get; set; }
    public string? CustomerName { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Address { get; set; }
}
