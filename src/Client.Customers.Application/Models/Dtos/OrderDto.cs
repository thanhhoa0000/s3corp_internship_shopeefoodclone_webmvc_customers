namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;

public class OrderDto
{
    public Guid Id { get; set; }
    [Required]
    public Guid CustomerId { get; set; }
    [Required]
    public Guid StoreId { get; set; }
    public string? StoreName { get; set; }
    [Required]
    [Range(1, double.MaxValue)]
    [Column(TypeName = "decimal(18,0)")]
    public decimal TotalPrice { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public OrderStatus OrderStatus { get; set; } = OrderStatus.Pending;
    [Required]
    public string? CustomerName { get; set; }
    [Required]
    [DataType(DataType.PhoneNumber)]
    public string? PhoneNumber { get; set; }
    [Required]
    public string? Address { get; set; }
    public ICollection<OrderDetailDto> OrderDetails { get; set; } = new List<OrderDetailDto>();
}
