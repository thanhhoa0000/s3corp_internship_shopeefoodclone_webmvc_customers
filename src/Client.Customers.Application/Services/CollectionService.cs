namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class CollectionService : ICollectionService
{
    private readonly IBaseService _service;

    public CollectionService(IBaseService service)
    { 
        _service = service; 
    }
    
    public async Task<Response?> GetCollectionsByLocationAndCategoryAsync(GetCollectionsRequest request)
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
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/collections?" +
                  $"province={province}" +
                  $"&district={district}" +
                  $"&ward={ward}" +
                  $"&category={category}" +
                  $"&pageSize={pageSize}" +
                  $"&pageNumber={pageNumber}"
        });
    }
}
