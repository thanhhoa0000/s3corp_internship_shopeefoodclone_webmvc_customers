namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;

public class MenuDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid StoreId { get; set; }
    [Required]
    [MaxLength(100)]
    public required string Title { get; set; }
    public MenuState State { get; set; } = MenuState.Active;
    public Guid ConcurrencyStamp { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
    public DateTime LastUpdatedAt { get; set; } = DateTime.UtcNow;
    public ICollection<ProductDto> Products { get; set; } = new List<ProductDto>();
}
