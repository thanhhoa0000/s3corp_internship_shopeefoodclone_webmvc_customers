namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public class StorePromotionsViewModel
{
    public List<StoreDto> Stores { get; set; } = new List<StoreDto>();
    public int PagesCount { get; set; } = 1;
    public int CurrentPage { get; set; } = 1;
    public int TotalStoresCount { get; set; } = 0;
}
