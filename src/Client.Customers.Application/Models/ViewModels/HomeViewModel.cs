namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class HomeViewModel
{
    public string? CategoryName { get; set; }
    public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    public List<StoreDto> Stores { get; set; } = new List<StoreDto>();
    public List<StoreDto> PromotionStores { get; set; } = new List<StoreDto>();
    public List<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
    public int StoresCount { get; set; }
    public int PageSize { get; set; } = 9;
}
