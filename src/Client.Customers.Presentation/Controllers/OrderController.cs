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
    public IActionResult List()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";

            return RedirectToAction("Login", "Account");
        }
        
        return View(new OrdersListViewModel());
    }

    [HttpPost]
    public async Task<IActionResult> List(Guid customerId)
    {
        try
        {
            var viewModel = new OrdersListViewModel();
            var orders = new List<OrderDto>();
            
            Response? ordersResponse = await _orderService.GetByCustomerIdAsync(new GetOrdersRequest
            {
                CustomerId = customerId
            });
            
            if (ordersResponse!.IsSuccessful && ordersResponse.Body is not null)
                orders = JsonSerializer.Deserialize<List<OrderDto>>(
                    Convert.ToString(ordersResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            
            viewModel.Orders = orders;
            
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return RedirectToAction("Index", "Home");
        }
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
            var viewModel = new OrderInitViewModel();

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
    public async Task<IActionResult> CreateOrder(OrderInitViewModel model)
    {
        try
        {
            var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            Response? cartResponse = await _cartService.GetCartAsync(Guid.Parse(customerId!));

            if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
                model.Cart = JsonSerializer.Deserialize<CartDto>(
                    Convert.ToString(cartResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            else
            {
                TempData["error"] = "Đã xảy ra lỗi!";

                return View(model);
            }

            Response? response = await _orderService.CreateOrderAsync(request: new CreateOrderRequest
            {
                Cart = model.Cart,
                CustomerName = model.CustomerName,
                Address = model.CustomerAddress,
                PhoneNumber = model.CustomerPhoneNumber
            });

            _logger.LogDebug($"\n-------\n{response!.Message}\n-------");

            if (response!.IsSuccessful)
            {
                var orders = new List<OrderDto>();
                
                Response? orderToReturn = await _orderService.GetByCustomerIdAsync(new GetOrdersRequest
                {
                    CustomerId = Guid.Parse(customerId!)
                });
                
                if (orderToReturn!.IsSuccessful && orderToReturn.Body is not null)
                    orders = JsonSerializer.Deserialize<List<OrderDto>>(
                        Convert.ToString(orderToReturn.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
                
                TempData["success"] = "Đặt hàng thành công!";

                return RedirectToAction("Details", "Order", new { orderId = orders.First().Id });
            }

            TempData["error"] = "Đã xảy ra lỗi!";

            return RedirectToAction("Index", "Home");
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return RedirectToAction("Index", "Home");
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid orderId)
    {
        if (!User.Identity!.IsAuthenticated)
        {
            TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";

            return RedirectToAction("Login", "Account");
        }

        try
        {
            var order = new OrderDto();
            var store = new StoreDto();
            var viewModel = new OrderDetailsViewModel();
            
            Response? orderResponse = await _orderService.GetByIdAsync(orderId);
            
            if (orderResponse!.IsSuccessful && orderResponse.Body is not null)
                order = JsonSerializer.Deserialize<OrderDto>(
                    Convert.ToString(orderResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            
            Response? storeResponse = await _storeService.GetStoreDetails(order.StoreId);

            if (storeResponse!.IsSuccessful && storeResponse.Body is not null)
                store = JsonSerializer.Deserialize<StoreDto>(
                    Convert.ToString(storeResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            
            viewModel.Order = order;
            viewModel.Store = store;
            
            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return RedirectToAction("Index", "Home");
        }
    }
}
