namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public class GetSubCategoriesRequest : BaseSearchRequest
{
    public Guid? CategoryId { get; set; }
    public string? CategoryName { get; set; }
}
