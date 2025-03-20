namespace ShopeeFoodClone.WebMvc.Customers.Application.Requests;

public sealed record CreateCollectionRequest(CollectionDto Collection, List<Guid> StoreIds);
