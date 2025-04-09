namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class GetOrdersRequest : BaseSearchRequest
{
    public Guid CustomerId { get; set; }
    public OrderStatus Status { get; set; }
    public SortingOrder SortingOrder { get; set; } = SortingOrder.Descending;
}
