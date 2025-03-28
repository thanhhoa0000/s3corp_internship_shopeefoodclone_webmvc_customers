namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public class StoreDetailsViewModel
{
    public StoreDto? Store { get; set; }
    public List<MenuDto> MenuItems { get; set; } = new List<MenuDto>();
    public List<ProductDto> Products { get; set; } = new List<ProductDto>();
    public string StarHtml { get; set; } = string.Empty;
}
