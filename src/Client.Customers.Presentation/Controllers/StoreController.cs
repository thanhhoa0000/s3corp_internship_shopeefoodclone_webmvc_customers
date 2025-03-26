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
    public async Task<IActionResult> Index(
        string province,
        string districtsString,
        string categoryName,
        string subcategoriesString,
        int pageSize = 30,
        int pageNumber = 1)
    {
        var stores = new List<StoreDto>();
        var districts = districtsString?.Split(",").ToList() ?? new List<string>();
        var subcategories = subcategoriesString?.Split(",").ToList() ?? new List<string>();

        Response? storesResponse = await _storeService.GetStoresByLocationAndCategoryAsync(
            request: new GetStoresRequest
            {
                LocationRequest = new LocationRequest
                {
                    Province = province,
                    Districts = districts
                },
                CategoryName = categoryName,
                SubCategoryNames = subcategories,
                PageSize = pageSize,
                PageNumber = pageNumber
            });

        if (storesResponse!.IsSuccessful)
            stores = JsonSerializer.Deserialize<List<StoreDto>>(
                Convert.ToString(storesResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new StorePromotionsViewModel
        {
            Stores = stores.Where(store => store.IsPromoted).ToList(),
            PagesCount = (int)Math.Ceiling((double)stores.Where(store => store.IsPromoted).ToList().Count / pageSize),
            CurrentPage = pageNumber
        };

        return View(viewModel);
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid storeId)
    {
        var store = new StoreDto();
        
        Response? storeResponse = await _storeService.GetStoreDetails(storeId);

        if (storeResponse!.IsSuccessful && storeResponse.Body is not null)
            store = JsonSerializer.Deserialize<StoreDto>(
                Convert.ToString(storeResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        else 
        {
            TempData["error"] = "Error occured when getting store details";

            return RedirectToAction("Index", "Home");
        }

        var viewModel = new StoreDetailsViewModel
        {
            Store = store
        };
        
        return View(viewModel);
    }
}
