namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class CategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required, MinLength(4), MaxLength(50)]
    public required string Name { get; set; }
    public Guid ConcurrencyStamp  { get; set; }
    
    public ICollection<StoreDto> Stores { get; set; } = new List<StoreDto>();
}