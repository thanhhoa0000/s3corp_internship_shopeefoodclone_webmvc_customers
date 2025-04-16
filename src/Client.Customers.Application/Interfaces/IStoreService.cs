namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IStoreService
{
    Task<Response?> GetStoresByLocationAndCategoryAsync(GetStoresRequest request);
    Task<Response?> GetStoreDetails(Guid storeId);
    Task<Response?> GetStoresCount(GetStoresCountRequest request);
}
