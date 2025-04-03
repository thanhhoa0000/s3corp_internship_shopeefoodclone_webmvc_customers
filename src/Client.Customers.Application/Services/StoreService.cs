namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class StoreService : IStoreService
{
    private readonly IBaseService _service;

    public StoreService(IBaseService service)
    {
        _service = service;
    }
    
    public async Task<Response?> GetStoresByLocationAndCategoryAsync(GetStoresRequest request)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores/get"
        }, bearer: false);
    }

    public async Task<Response?> GetStoreDetails(Guid storeId)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores/{storeId}"
        }, bearer: false);
    }

    public async Task<Response?> GetStoresCount()
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores/get-count"
        }, bearer: false);
    }
}
