namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface IBaseService
{
    Task<Response?> SendAsync(Request request, bool bearer = true);
}