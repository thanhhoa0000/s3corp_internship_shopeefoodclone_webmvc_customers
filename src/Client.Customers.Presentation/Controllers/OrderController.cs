namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(
        IOrderService orderService,
        ICartService cartService,
        ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _cartService = cartService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> OrderInit(Guid customerId)
    {
        var cart = new CartDto();
        var viewModel = new OrderDetailsViewModel();


        Response? cartResponse = await _cartService.GetCartAsync(customerId);

        if (cartResponse!.Message.Contains("The cart is empty"))
        {
            return View(viewModel);
        }

        if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
            cart = JsonSerializer.Deserialize<CartDto>(
                Convert.ToString(cartResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        viewModel.Cart = cart;

        return View(viewModel);
    }
}
