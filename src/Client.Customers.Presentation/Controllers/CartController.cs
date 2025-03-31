namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class CartController : Controller
{
    private readonly ICartService _cartService;
    private readonly ILogger<CartController> _logger;

    public CartController(
        ICartService cartService,
        ILogger<CartController> logger)
    {
        _cartService = cartService;
        _logger = logger;
    }
    
    [HttpGet]
    public async Task<IActionResult> Index(Guid customerId)
    {
        var cart = new CartDto();
        
        Response? cartResponse = await _cartService.GetCartAsync(customerId);
        
        if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
            cart = JsonSerializer.Deserialize<CartDto>(
                Convert.ToString(cartResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        else
        {
            TempData["error"] = cartResponse.Message;
            
            return RedirectToAction("Index", "Home");
        }

        var viewModel = new CartViewModel
        {
            Cart = cart
        };
        
        return View(viewModel);
    }
}