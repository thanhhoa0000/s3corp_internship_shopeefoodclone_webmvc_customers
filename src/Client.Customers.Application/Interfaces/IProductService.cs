namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IProductService
{
    Task<Response?> GetProductsByStoreIdAsync(GetProductsRequest request);   
}
