namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class CartService : ICartService
{
    private readonly IBaseService _service;

    public CartService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetCartAsync(Guid customerId)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/cart/user/{customerId}"
        }, bearer: true);
    }
}