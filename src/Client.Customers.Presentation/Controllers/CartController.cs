namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly IStoreService _storeService;
    private readonly ILogger<CartController> _logger;

    public CartController(
        ICartService cartService,
        IStoreService storeService,
        ILogger<CartController> logger)
    {
        _cartService = cartService;
        _storeService = storeService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(Guid customerId)
    {
        var cart = new CartDto();
        var store = new StoreDto();
        
        var viewModel = new CartViewModel
        {
            Cart = cart,
            Store = store,
        };
        
        Response? cartResponse = await _cartService.GetCartAsync(customerId);

        if (cartResponse!.Message.Contains("The cart is empty"))
        {
            return View(viewModel);
        }
        
        if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
            cart = JsonSerializer.Deserialize<CartDto>(
                Convert.ToString(cartResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        Response? storeResponse = await _storeService.GetStoreDetails(cart.CartHeader!.StoreId);

        if (storeResponse!.IsSuccessful && storeResponse.Body is not null)
            store = JsonSerializer.Deserialize<StoreDto>(
                Convert.ToString(storeResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        
        else
        {
            TempData["error"] = cartResponse.Message;
            
            return RedirectToAction("Index", "Home");
        }

        viewModel.Cart = cart;
        viewModel.Store = store;
        
        return View(viewModel);
    }
}
