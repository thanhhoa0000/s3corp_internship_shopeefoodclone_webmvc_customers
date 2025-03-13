namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ICategoryService _categoryService;
    private readonly IStoreService _storeService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ISubCategoryService subCategoryService,
        ICategoryService categoryService,
        IStoreService storeService, 
        ILogger<HomeController> logger)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;
        _storeService = storeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var categories = new List<CategoryDto>();
        
        Response? categoriesResponse = await _categoryService.GetAllAsync();
        
        if (categoriesResponse!.IsSuccessful)
            categories = JsonSerializer.Deserialize<List<CategoryDto>>(
                JsonSerializer.Serialize(categoriesResponse.Body),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        var viewModel = new HomeViewModel(){ Categories = categories };
        
        return View(viewModel);
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string province, string categoryName, int pageSize = 9, int pageNumber = 1)
    {
        var subCategories = new List<SubCategoryDto>();
        var stores = new List<StoreDto>();
        
        _logger.LogDebug(categoryName);
        
        Response? subCategoriesResponse = await _subCategoryService.GetAllByCategoryNameAsync(categoryName);
        
        if (subCategoriesResponse!.IsSuccessful)
            subCategories = JsonSerializer.Deserialize<List<SubCategoryDto>>(
                JsonSerializer.Serialize(subCategoriesResponse.Body),
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        _logger.LogDebug(subCategoriesResponse.Body!.ToString());
        
        Response? storesResponse = await _storeService.GetStoresByLocationAndCategoryAsync(
            request: new GetStoreRequest(
                LocationRequest: new GetStoreByLocationRequest(Province: province, null, null),
                CategoryName: categoryName),
            pageSize: pageSize,
            pageNumber: pageNumber);
        
        if (storesResponse!.IsSuccessful)
            stores = JsonSerializer.Deserialize<List<StoreDto>>(
                Convert.ToString(storesResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        var viewModel = new HomeViewModel()
        {
            SubCategories = subCategories,
            CategoryName = subCategories.First().Category!.Name,
            Stores = stores,
            StoresCount = stores?.Count ?? 0
        };
        
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