namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class HomeViewModel
{
    public List<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
    public List<StoreDto> Stores { get; set; } = new List<StoreDto>();
}