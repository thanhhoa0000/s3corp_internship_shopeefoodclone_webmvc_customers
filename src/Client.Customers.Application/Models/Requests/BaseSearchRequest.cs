namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public class BaseSearchRequest
{
    [Required] 
    public int PageSize { get; set; } = 0;
    [Required] 
    public int PageNumber { get; set; } = 1;
    public string? SearchText { get; set; }
}
