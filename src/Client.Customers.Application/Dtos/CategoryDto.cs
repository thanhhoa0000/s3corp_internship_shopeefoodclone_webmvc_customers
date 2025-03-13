namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MinLength(2), MaxLength(50)]
    public string? Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public string? CodeName { get; set; }
    public Guid ConcurrencyStamp  { get; set; }
    
    public ICollection<StoreDto> Stores { get; set; } = new List<StoreDto>();
}