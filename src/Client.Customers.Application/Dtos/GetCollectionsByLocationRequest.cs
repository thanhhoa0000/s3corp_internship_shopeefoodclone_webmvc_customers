namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public sealed record GetCollectionsByLocationRequest(string? Province, string? District, string? Ward);
