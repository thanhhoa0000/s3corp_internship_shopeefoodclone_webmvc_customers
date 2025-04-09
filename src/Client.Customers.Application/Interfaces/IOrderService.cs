namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IOrderService
{
    Task<Response?> GetByCustomerIdAsync(GetOrdersRequest request);
    Task<Response?> GetByIdAsync(Guid orderId);
    Task<Response?> CreateOrderAsync(CreateOrderRequest request);
}
