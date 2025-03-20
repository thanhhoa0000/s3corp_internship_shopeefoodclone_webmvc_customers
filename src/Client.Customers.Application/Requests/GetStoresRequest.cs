namespace ShopeeFoodClone.WebMvc.Customers.Application.Requests;

public sealed class GetStoresRequest : BaseSearchRequest
{
    [Required]
    public LocationRequest? LocationRequest { get; set; }
    public string? CategoryName { get; set; }
}
