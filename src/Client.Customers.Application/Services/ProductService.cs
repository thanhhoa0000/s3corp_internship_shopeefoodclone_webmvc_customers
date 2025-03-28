namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class ProductService : IProductService
{
    private readonly IBaseService _service;

    public ProductService(IBaseService service)
    {
        _service = service;
    }
    
    public async Task<Response?> GetProductsByStoreIdAsync(GetProductsRequest request)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/products/get-from-store"
        });
    }

    public async Task<Response?> GetProductByIdAsync(Guid productId)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/products/{productId}"
        });
    }
}
