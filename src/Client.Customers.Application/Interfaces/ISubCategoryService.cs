namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ISubCategoryService
{
    Task<Response?> GetAllByCategoryIdAsync(GetSubCategoriesRequest request);
    Task<Response?> GetAllByCategoryNameAsync(GetSubCategoriesRequest request);
}
