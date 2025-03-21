namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class SubCategoryService : ISubCategoryService
{
    private readonly IBaseService _service;

    public SubCategoryService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetAllByCategoryIdAsync(GetSubCategoriesRequest request)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/categories/sub-categories/get-by-cateId",
        }, bearer: false);
    }
    
    public async Task<Response?> GetAllByCategoryNameAsync(GetSubCategoriesRequest request)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/categories/sub-categories/get-by-cateName",
        }, bearer: false);
    }
}
