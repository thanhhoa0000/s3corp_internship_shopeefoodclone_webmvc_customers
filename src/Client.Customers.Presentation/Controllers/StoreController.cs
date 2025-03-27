namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class StoreController : Controller
{
    private readonly IStoreService _storeService;
    private readonly IProductService _productService;
    private readonly ILogger<StoreController> _logger;

    public StoreController(
        IStoreService storeService,
        IProductService productService,
        ILogger<StoreController> logger)
    {
        _storeService = storeService;
        _productService = productService;
        _logger = logger;
    }
    [HttpGet]
    public IActionResult Promotions() => View(new StorePromotionsViewModel());

    [HttpPost]
    public async Task<IActionResult> Promotions(
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
        var products = new List<ProductDto>();
        
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

        Response? productsResponse = await _productService.GetProductsByStoreIdAsync(
            request: new GetProductsRequest
            {
                StoreId = store.Id
            });
        
        if (productsResponse!.IsSuccessful)
            products = JsonSerializer.Deserialize<List<ProductDto>>(
                Convert.ToString(productsResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new StoreDetailsViewModel
        {
            Store = store,
            Products = products
        };
        
        return View(viewModel);
    }
}
