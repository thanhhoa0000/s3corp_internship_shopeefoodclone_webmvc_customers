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
    public ActionResult Index()
    {
        return View();
    }
}
