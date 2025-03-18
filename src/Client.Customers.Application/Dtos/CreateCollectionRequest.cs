namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public sealed record CreateCollectionRequest(CollectionDto Collection, List<Guid> StoreIds);
