namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public class BaseSearchRequest
{
    [Required]
    public int PageSize { get; set; }
    [Required]
    public int PageNumber { get; set; }
    public string? SearchText { get; set; }
}
