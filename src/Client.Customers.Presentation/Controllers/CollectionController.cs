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
    public async Task<ActionResult> Index(string province, string categoryName, int pageSize = 15, int pageNumber = 1)
    {
        try
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
                CollectionsCount = collections.Count,
                CurrentPage = pageNumber
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex.ToString()}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new CollectionsViewModel());
        }
    }
}
