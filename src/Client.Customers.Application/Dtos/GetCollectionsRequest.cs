namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public sealed record GetCollectionsRequest(GetCollectionsByLocationRequest LocationRequest, string CategoryName);
