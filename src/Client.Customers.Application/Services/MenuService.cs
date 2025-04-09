namespace ShopeeFoodClone.WebMvc.Customers.Application.Services;

public class MenuService : IMenuService
{
    private readonly IBaseService _service;

    public MenuService(IBaseService service)
    {
        _service = service;
    }

    public async Task<Response?> GetMenuItemsByStoreIdAsync(GetMenusRequest request)
    {
        return await _service.SendAsync(new Request()
        {
            ApiMethod = ApiMethod.Post,
            Body = request,
            Url = $"{ApiUrlProperties.ApiGatewayUrl}/menus/get-from-store"
        }, bearer: false);
    }    
}
