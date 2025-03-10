namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class DistrictDto
{
    [MaxLength(20)]
    public required string Code { get; set; }
    [MaxLength(20)]
    public string? ProvinceCode { get; set; }
    [Required]
    public required string Name { get; set; }
    public string? NameEng { get; set; }
    [Required]
    public required string FullName { get; set; }
    public string? FullNameEng { get; set; }
    public string? CodeName { get; set; }
    public byte? AdministrativeUnitId { get; set; }
    
    public ProvinceDto? Province { get; set; }
    public AdministrativeUnitDto? AdministrativeUnit { get; set; }
}