namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class CollectionService : ICollectionService
{
    private readonly IBaseService _service;

    public CollectionService(IBaseService service)
    { 
        _service = service; 
    }
    
    public async Task<Response?> GetCollectionsByLocationAsync(GetCollectionsByLocationRequest request, int pageSize, int pageNumber)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/collections/{request.Province}?pageSize={pageSize}&pageNumber={pageNumber}"
        });
    }
    
    public async Task<Response?> GetCollectionsByLocationAndCategoryAsync(GetCollectionsRequest request, int pageSize, int pageNumber)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/collections/{request.LocationRequest.Province}/{request.CategoryName}?pageSize={pageSize}&pageNumber={pageNumber}"
        });
    }
}
