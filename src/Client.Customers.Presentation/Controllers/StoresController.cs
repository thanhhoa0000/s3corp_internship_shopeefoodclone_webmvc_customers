namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class StoresController : Controller
{
    private readonly IStoreService _service;
    private readonly ILogger<StoresController> _logger;

    public StoresController(IStoreService service, ILogger<StoresController> logger)
    {
        _service = service;
        _logger = logger;
    }
        
    // GET: StoresController
    public ActionResult Index()
    {
        return View();
    }

}
