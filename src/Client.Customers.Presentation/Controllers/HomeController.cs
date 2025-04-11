namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ISubCategoryService _subCategoryService;
    private readonly ICategoryService _categoryService;
    private readonly IStoreService _storeService;
    private readonly ICollectionService _collectionService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        ISubCategoryService subCategoryService,
        ICategoryService categoryService,
        IStoreService storeService,
        ICollectionService collectionService,
        ILogger<HomeController> logger)
    {
        _subCategoryService = subCategoryService;
        _categoryService = categoryService;
        _storeService = storeService;
        _collectionService = collectionService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            var categories = new List<CategoryDto>();

            Response? categoriesResponse = await _categoryService.GetAllAsync();

            if (categoriesResponse!.IsSuccessful)
                categories = JsonSerializer.Deserialize<List<CategoryDto>>(
                    JsonSerializer.Serialize(categoriesResponse.Body),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var viewModel = new HomeViewModel() { Categories = categories };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex.ToString()}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new HomeViewModel());
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string province, string districtsString, string categoryName, int pageSize = 9, int pageNumber = 1)
    {
        try
        {
            var subCategories = new List<SubCategoryDto>();
            var stores = new List<StoreDto>();
            var collections = new List<CollectionDto>();
            var districts = districtsString?.Split(",").ToList() ?? new List<string>();
            var storesCount = 0;

            Response? subCategoriesResponse = await _subCategoryService.GetAllByCategoryNameAsync(
                request: new GetSubCategoriesRequest
                {
                    CategoryName = categoryName
                });

            if (subCategoriesResponse!.IsSuccessful)
                subCategories = JsonSerializer.Deserialize<List<SubCategoryDto>>(
                    JsonSerializer.Serialize(subCategoriesResponse.Body),
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            Response? storesResponse = await _storeService.GetStoresByLocationAndCategoryAsync(
                request: new GetStoresRequest
                {
                    LocationRequest = new LocationRequest
                    {
                        Province = province,
                        Districts = districts
                    },
                    CategoryName = categoryName,
                    PageSize = pageSize,
                    PageNumber = pageNumber
                });

            if (storesResponse!.IsSuccessful)
                stores = JsonSerializer.Deserialize<List<StoreDto>>(
                    Convert.ToString(storesResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            Response? storesCountResponse = await _storeService.GetStoresCount(new GetStoresCountRequest
            {
                LocationRequest = new LocationRequest { Province = province },
                IsPromoted = false
            });

            if (storesCountResponse!.IsSuccessful)
                storesCount = JsonSerializer.Deserialize<int>(
                    Convert.ToString(storesCountResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

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

            var viewModel = new HomeViewModel()
            {
                SubCategories = subCategories,
                CategoryName = subCategories.First().Category!.Name,
                Stores = stores,
                PromotionStores = stores.Where(store => store.IsPromoted).ToList(),
                Collections = collections,
                StoresCount = storesCount
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new HomeViewModel());
        }
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
