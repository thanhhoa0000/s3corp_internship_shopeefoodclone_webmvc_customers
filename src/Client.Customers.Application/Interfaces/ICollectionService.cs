namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ICollectionService
{
    Task<Response?> GetCollectionsByLocationAsync(GetCollectionsByLocationRequest request, int pageSize, int pageNumber);
    Task<Response?> GetCollectionsByLocationAndCategoryAsync(GetCollectionsRequest request, int pageSize, int pageNumber);
}
