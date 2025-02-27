namespace ShopeeFoodClone.WebMvc.Customers.Interfaces;

public interface IBaseService
{
    Task<Response?> SendAsync(Request request, bool bearer = true);
}