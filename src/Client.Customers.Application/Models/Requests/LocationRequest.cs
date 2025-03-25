namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class LocationRequest
{
    public string? Province { get; set; }
    public List<string>? Districts { get; set; }
    public List<string>? Wards { get; set; }
}
