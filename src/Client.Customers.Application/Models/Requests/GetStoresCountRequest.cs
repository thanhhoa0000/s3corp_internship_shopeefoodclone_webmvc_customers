namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class GetStoresCountRequest : BaseSearchRequest
{
    public LocationRequest? LocationRequest { get; set; }
    public string? CategoryName { get; set; }
    public List<string>? SubCategoryNames { get; set; }
    public bool IsPromoted { get; set; } = false;
    public Guid VendorId { get; set; }
}
