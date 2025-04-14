namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class ProductDetailsViewModel
{
    public ProductDto? Product { get; set; }
    public string StartHtml { get; set; } = string.Empty;
}
