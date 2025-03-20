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
        var province = request.LocationRequest!.Province;
        var district = request.LocationRequest!.District;
        var ward = request.LocationRequest!.Ward;
        var category = request.CategoryName;
        var pageSize = request.PageSize;
        var pageNumber = request.PageNumber;
        
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/stores?" +
                  $"province={province}" +
                  $"&district={district}" +
                  $"&ward={ward}" +
                  $"&category={category}" +
                  $"&pageSize={pageSize}" +
                  $"&pageNumber={pageNumber}"
        });
    }
}
