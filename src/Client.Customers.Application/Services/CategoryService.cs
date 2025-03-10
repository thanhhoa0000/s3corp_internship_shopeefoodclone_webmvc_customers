namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class CategoryService : ICategoryService
{
    private readonly IBaseService _service;

    public CategoryService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetAllAsync()
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Get,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/categories"
        }, bearer: false);
    }
}