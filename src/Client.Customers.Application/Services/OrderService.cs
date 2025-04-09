namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class OrderService : IOrderService
{
    private readonly IBaseService _service;

    public OrderService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> CreateOrderAsync(CreateOrderRequest request)
    {
        return await _service.SendAsync(new Request
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/orders"
        }, bearer: true);
    }

    public async Task<Response?> GetByIdAsync(Guid orderId)
    {
        return await _service.SendAsync(new Request
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/orders/{orderId}"
        }, bearer: true);
    }
    
    public async Task<Response?> GetByCustomerIdAsync(GetOrdersRequest request)
    {
        return await _service.SendAsync(new Request
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/orders/get"
        }, bearer: true);
    }
}
