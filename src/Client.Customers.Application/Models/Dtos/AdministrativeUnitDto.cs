namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;

public class AdministrativeUnitDto
{
    public byte Id { get; set; }
    public string? FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? ShortName { get; set; }
    public string? ShortNameEng { get; set; }
    public string? CodeName { get; set; }
    public string? CodeNameEng { get; set; }
}