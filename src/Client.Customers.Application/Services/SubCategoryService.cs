namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly IBaseService _service;

    public SubCategoryService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetAllByCategoryIdAsync(Guid cateId)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/categories/{cateId}/sub-categories",
        }, bearer: false);
    }
    
    public async Task<Response?> GetAllByCategoryNameAsync(string cateName)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/categories/{cateName}/sub-categories",
        }, bearer: false);
    }
}
