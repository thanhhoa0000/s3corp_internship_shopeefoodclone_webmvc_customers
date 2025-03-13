namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ICategoryService
{
    Task<Response?> GetAllAsync();
    Task<Response?> GetByCodeNameAsync(string name);
}