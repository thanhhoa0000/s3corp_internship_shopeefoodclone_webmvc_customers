namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class StoreController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ILogger<StoreController> _logger;

    public StoreController(
        IStoreService storeService,
        ILogger<StoreController> logger)
    {
        _storeService = storeService;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Index() => View(new StorePromotionsViewModel());
    
    [HttpPost]
    public async Task<ActionResult> Index(string province, string categoryName, int pageSize = 30, int pageNumber = 1)
    {
        var stores = new List<StoreDto>();
        
        Response? storesResponse = await _storeService.GetStoresByLocationAndCategoryAsync(
            request: new GetStoresRequest
            {
                LocationRequest = new LocationRequest { Province = province },
                CategoryName = categoryName,
                PageSize = pageSize,
                PageNumber = pageNumber
            });
        
        if (storesResponse!.IsSuccessful)
            stores = JsonSerializer.Deserialize<List<StoreDto>>(
                Convert.ToString(storesResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new StorePromotionsViewModel
        {
            Stores = stores,
        };
        
        return View(viewModel);
    }
}
