namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;
public class ProductDto
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid StoreId { get; set; }
    [Required, MinLength(10), MaxLength(50)]
    public string? Name { get; set; }
    [Required, MinLength(20), MaxLength(200)]
    public string Description { get; set; } = string.Empty;

    [Required] public int AvailableStock { get; set; } = 0;

    public int BookingCount { get; set; } = 0;
    public int RateCount { get; set; } = 0;
    public double Rating { get; set; } = 0.0;
    public string? CoverImagePath { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    public decimal Price { get; set; }
    public decimal Discount { get; set; }
    public Guid ConcurrencyStamp { get; set; }
    public ProductState State { get; set; } = ProductState.Normal;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? LastUpdatedAt { get; set; }
    public ICollection<MenuDto> Menus { get; set; } = new List<MenuDto>();
}
