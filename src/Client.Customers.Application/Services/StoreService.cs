namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class StoreService : IStoreService
{
    private readonly IBaseService _service;

    public StoreService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetStoresByLocationAsync(GetStoreByLocationRequest request, int pageSize, int pageNumber)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores/location/{request.Province}?pageSize={pageSize}&pageNumber={pageNumber}"
        });
    }
    
    public async Task<Response?> GetStoresByLocationAndCategoryAsync(GetStoreRequest request, int pageSize, int pageNumber)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores/location/{request.LocationRequest.Province}/category/{request.CategoryName}?pageSize={pageSize}&pageNumber={pageNumber}"
        });
    }
}
