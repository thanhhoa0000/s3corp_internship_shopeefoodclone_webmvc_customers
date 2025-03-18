namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class CollectionDto
{
    public Guid Id { get; set; }
    [Required, MaxLength(150)]
    public required string Name { get; set; }
    public string? CoverImagePath { get; set; }
    public DateOnly StartDate { get; set; }
    public CollectionState State { get; set; } = CollectionState.Upcoming;
    
    public ICollection<StoreDto> Stores { get; set; } = new List<StoreDto>();
}