namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class StoreDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    [Required]
    public Guid UserId { get; set; }
    [MaxLength(20)]
    public string? WardCode { get; set; }
    [MaxLength(100)]
    public string? StreetName { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public required string Name { get; set; }
    [Required]
    public TimeOnly OpeningHour { get; set; }
    [Required]
    public TimeOnly ClosingHour { get; set; }
    public string? CoverImagePath { get; set; }
    public double Rating { get; set; } = 0.0;
    
    public WardDto? Ward { get; set; }
    public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
}