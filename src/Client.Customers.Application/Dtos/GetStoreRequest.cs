namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public sealed record GetStoreRequest(GetStoreByLocationRequest LocationRequest, string CategoryName);
