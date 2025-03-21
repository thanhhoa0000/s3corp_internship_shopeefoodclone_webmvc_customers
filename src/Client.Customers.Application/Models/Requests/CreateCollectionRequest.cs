namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed record CreateCollectionRequest(CollectionDto Collection, List<Guid> StoreIds);
