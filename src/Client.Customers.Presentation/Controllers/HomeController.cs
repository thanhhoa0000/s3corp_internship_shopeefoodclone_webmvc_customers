namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly IStoreService _storeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICategoryService categoryService, IStoreService storeService, ILogger<HomeController> logger)
    {
        _categoryService = categoryService;
        _storeService = storeService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult Index() => View(new HomeViewModel());
    
    [HttpPost]
    public async Task<IActionResult> Index(string province, int pageSize = 9, int pageNumber = 1)
    {
        var categories = new List<CategoryDto>();
        var stores = new List<StoreDto>();
        
        Response? categoriesResponse = await _categoryService.GetAllAsync();
        
        if (categoriesResponse!.IsSuccessful)
            categories = JsonSerializer.Deserialize<List<CategoryDto>>(
                JsonSerializer.Serialize(categoriesResponse.Body),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        Response? storesResponse = await _storeService.GetStoresByLocationAsync(
            request: new GetStoreByLocationRequest(Province: province, null, null),
            pageSize: pageSize,
            pageNumber: pageNumber);
        
        if (storesResponse!.IsSuccessful)
            stores = JsonSerializer.Deserialize<List<StoreDto>>(
                Convert.ToString(storesResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new HomeViewModel()
        {
            Categories = categories,
            Stores = stores,
        };
        
        _logger.LogDebug(viewModel.Stores.Count().ToString());
        
        return View(viewModel);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}