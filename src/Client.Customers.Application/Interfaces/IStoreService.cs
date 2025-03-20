namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IStoreService
{
    Task<Response?> GetStoresByLocationAndCategoryAsync(GetStoresRequest request);
}