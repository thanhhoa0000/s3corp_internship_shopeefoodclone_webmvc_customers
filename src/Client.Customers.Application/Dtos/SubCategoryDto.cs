namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class SubCategoryDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CategoryId { get; set; }

    [Required, MinLength(2), MaxLength(50)]
    public required string Name { get; set; }
    [Required, MinLength(2), MaxLength(50)]
    public required string CodeName { get; set; }
    public Guid ConcurrencyStamp { get; set; }
    
    public CategoryDto? Category { get; set; }
}
