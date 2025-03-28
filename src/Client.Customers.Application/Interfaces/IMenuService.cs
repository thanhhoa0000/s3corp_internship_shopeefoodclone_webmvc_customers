namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IMenuService
{
    Task<Response?> GetMenuItemsByStoreIdAsync(GetMenusRequest request);
}
