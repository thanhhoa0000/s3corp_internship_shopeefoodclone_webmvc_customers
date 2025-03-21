namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;

public class WardDto
{
    [MaxLength(20)]
    public required string Code { get; set; }
    [MaxLength(20)]
    public string? DistrictCode { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? NameEng { get; set; }
    [Required]
    public required string FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? CodeName { get; set; }
    public byte? AdministrativeUnitId { get; set; }
    
    public DistrictDto? District { get; set; }
    public AdministrativeUnitDto? AdministrativeUnit { get; set; }
}