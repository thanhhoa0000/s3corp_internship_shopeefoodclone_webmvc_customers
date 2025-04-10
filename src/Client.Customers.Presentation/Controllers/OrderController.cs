namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly ICartService _cartService;
    private readonly IStoreService _storeService;
    private readonly ILogger<OrderController> _logger;

    public OrderController(
        IOrderService orderService,
        ICartService cartService,
        IStoreService storeService,
        ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _cartService = cartService;
        _storeService = storeService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> OrderInit(Guid customerId)
    {
        try
        {
            if (!User.Identity!.IsAuthenticated)
            {
                TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";

                return RedirectToAction("Login", "Account");
            }

            var cart = new CartDto();
            var store = new StoreDto();
            var viewModel = new OrderDetailsViewModel();

            Response? cartResponse = await _cartService.GetCartAsync(customerId);

            if (cartResponse!.Message.Contains("The cart is empty"))
            {
                return RedirectToAction("CartEmpty", "Cart");
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

            viewModel.Cart = cart;
            viewModel.Store = store;

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return RedirectToAction("Index", "Cart", new { customerId = customerId });
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> OrderInit(OrderDetailsViewModel model)
    {
        Response? response = await _orderService.CreateOrderAsync(request: new CreateOrderRequest
        {
            Cart = model.Cart,
            CustomerName = model.CustomerName,
            Address = model.CustomerAddress,
            PhoneNumber = model.CustomerPhoneNumber
        });

        if (response!.IsSuccessful)
        {
            TempData["success"] = "Đặt hàng thành công!";
            
            return RedirectToAction("Index", "Home");
        }
        
        TempData["error"] = "Đã xảy ra lỗi!";

        return View(model);
    }
}
