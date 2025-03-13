namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ISubCategoryService
{
    Task<Response?> GetAllByCategoryIdAsync(Guid cateId);
    Task<Response?> GetAllByCategoryNameAsync(string cateName);
}
