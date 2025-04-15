namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class CollectionsViewModel
{
    public List<CollectionDto> Collections { get; set; } = new List<CollectionDto>();
    public int CollectionsCount { get; set; } = 0;
    public int PagesCount { get; set; } = 1;
    public int CurrentPage { get; set; } = 1;
}
