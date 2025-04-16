namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;

public class OrderDetailDto
{
    public Guid Id { get; set; }
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public ProductDto? Product { get; set; }
    [Required]
    public string? ProductName { get; set; }
    [Required]
    public int Quantity { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal Price { get; set; }
    public OrderDto? Order { get; set; }
}
