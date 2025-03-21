namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class LocationRequest
{
    public string? Province { get; set; }
    public string? District { get; set; }
    public string? Ward { get; set; }
}
