namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class GetProductsRequest : BaseSearchRequest
{
    public Guid StoreId { get; set; }
}
