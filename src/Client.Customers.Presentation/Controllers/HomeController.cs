namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class HomeController : Controller
{
    private readonly ICategoryService _categoryService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(ICategoryService categoryService, ILogger<HomeController> logger)
    {
        _categoryService = categoryService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var categories = new List<CategoryDto>();
        
        Response? response = await _categoryService.GetAllAsync();
        
        if (response!.IsSuccessful)
            categories = JsonSerializer.Deserialize<List<CategoryDto>>(
                Convert.ToString(response.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        return View(categories);
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