namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class HomeViewModel
{
    public string? CategoryName { get; set; }
    public List<SubCategoryDto> SubCategories { get; set; } = new List<SubCategoryDto>();
    public List<StoreDto> Stores { get; set; } = new List<StoreDto>();
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public int StoresCount { get; set; }
}
