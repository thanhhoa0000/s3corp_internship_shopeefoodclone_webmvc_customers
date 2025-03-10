namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}