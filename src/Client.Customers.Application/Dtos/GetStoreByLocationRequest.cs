namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public sealed record GetStoreByLocationRequest(string? Province, string? District, string? Ward);
