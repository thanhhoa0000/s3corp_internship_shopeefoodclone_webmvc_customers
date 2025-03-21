namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class ErrorViewModel
{
    public string? RequestId { get; set; }

    public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
}
