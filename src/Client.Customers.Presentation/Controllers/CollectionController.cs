namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class CollectionController : Controller
{
    private readonly ICollectionService _collectionService;
    private readonly ILogger<CollectionController> _logger;

    public CollectionController(
        ICollectionService collectionService,
        ILogger<CollectionController> logger)
    {
        _collectionService = collectionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult> Index(string province, string categoryName, int pageSize = 30, int pageNumber = 1)
    {
        var collections = new List<CollectionDto>();

        Response? collectionsResponse = await _collectionService.GetCollectionsByLocationAndCategoryAsync(
            request: new GetCollectionsRequest
            {
                LocationRequest = new LocationRequest { Province = province },
                CategoryName = categoryName,
                PageSize = pageSize,
                PageNumber = pageNumber
            });

        if (collectionsResponse!.IsSuccessful)
            collections = JsonSerializer.Deserialize<List<CollectionDto>>(
                Convert.ToString(collectionsResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new CollectionsViewModel()
        {
            Collections = collections,
            PagesCount = (int)Math.Ceiling((double)(collections.Count) / pageSize),
            CurrentPage = pageNumber
        };

        return View(viewModel);
    }
}