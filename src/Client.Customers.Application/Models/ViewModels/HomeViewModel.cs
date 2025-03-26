namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public class HomeViewModel
{
    public string? CategoryName { get; set; }
    public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    public List<StoreDto> Stores { get; set; } = new List<StoreDto>();
    public List<StoreDto> PromotionStores { get; set; } = new List<StoreDto>();
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public List<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
    public int StoresCount { get; set; }
}
