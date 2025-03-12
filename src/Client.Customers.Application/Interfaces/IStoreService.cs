namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IStoreService
{
    Task<Response?> GetStoresByLocationAsync(GetStoreByLocationRequest request, int pageSize, int pageNumber);
}