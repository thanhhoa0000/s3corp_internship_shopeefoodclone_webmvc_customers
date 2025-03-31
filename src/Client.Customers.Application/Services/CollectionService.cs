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
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/collections/get"
        }, bearer: false);
    }
}
