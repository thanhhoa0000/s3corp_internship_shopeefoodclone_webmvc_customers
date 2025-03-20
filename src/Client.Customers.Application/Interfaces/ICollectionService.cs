namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ICollectionService
{
    Task<Response?> GetCollectionsByLocationAndCategoryAsync(GetCollectionsRequest request);
}
