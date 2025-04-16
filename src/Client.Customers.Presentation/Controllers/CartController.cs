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
        try
        {
            if (!User.Identity!.IsAuthenticated)
            {
                TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";

                return RedirectToAction("Login", "Account");
            }

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
                return RedirectToAction("CartEmpty", "Cart");
            }

            if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
                cart = JsonSerializer.Deserialize<CartDto>(
                    Convert.ToString(cartResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            else
            {
                TempData["error"] = "Có lỗi xảy ra khi truy cập giỏ hàng!";
                _logger.LogError(cartResponse.Message);

                return RedirectToAction("Index", "Home");
            }

            Response? storeResponse = await _storeService.GetStoreDetails(cart.CartHeader!.StoreId);

            if (storeResponse!.IsSuccessful && storeResponse.Body is not null)
                store = JsonSerializer.Deserialize<StoreDto>(
                    Convert.ToString(storeResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            else
            {
                TempData["error"] = "Có lỗi xảy ra khi truy cập giỏ hàng!";

                return RedirectToAction("Index", "Home");
            }

            viewModel.Cart = cart;
            viewModel.Store = store;

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex.ToString()}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new CartViewModel());
        }
    }

    [HttpGet]
    public IActionResult CartEmpty()
    {
        if (!User.Identity!.IsAuthenticated)
        {
            TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";

            return RedirectToAction("Login", "Account");
        }
        
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(Guid customerId, Guid productId, int quantity)
    {
        try
        {
            var viewModel = new CartItemQuantityPartialViewModel();
            var cart = new CartDto();

            var request = new AddToCartRequest
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            };

            await _cartService.AddToCartAsync(request);

            var cartResponse = await _cartService.GetCartAsync(customerId);
            
            if (cartResponse!.Message.Contains("The cart is empty"))
            {
                return Json(new { isCartEmpty = true });
            }

            if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
                cart = JsonSerializer.Deserialize<CartDto>(
                    Convert.ToString(cartResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            if (cart.CartHeader is not null)
                viewModel.CartHeader = cart.CartHeader;
            else
                viewModel.CartHeader = new CartHeaderDto();
            
            if (cartItem is not null)
                viewModel.CartItem = cartItem;
            else
                viewModel.CartItem = null;

            return PartialView("_CartItemQuantityPartial", viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return RedirectToAction("Index", "Cart", new { CustomerId = customerId });
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> AddToCartInStoreDetails(Guid customerId, Guid productId, int quantity)
    {
        try
        {
            var viewModel = new CartItemQuantityPartialViewModel();
            var cart = new CartDto();

            var request = new AddToCartRequest
            {
                CustomerId = customerId,
                ProductId = productId,
                Quantity = quantity
            };

            await _cartService.AddToCartAsync(request);

            var cartResponse = await _cartService.GetCartAsync(customerId);
            
            if (cartResponse!.Message.Contains("The cart is empty"))
            {
                return Json(new { isCartEmpty = true });
            }

            if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
                cart = JsonSerializer.Deserialize<CartDto>(
                    Convert.ToString(cartResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductId == productId);

            if (cart.CartHeader is not null)
                viewModel.CartHeader = cart.CartHeader;
            else
                viewModel.CartHeader = new CartHeaderDto();
            
            if (cartItem is not null)
                viewModel.CartItem = cartItem;
            else
                viewModel.CartItem = null;

            return PartialView("_CartItemQuantityStoreDetailsPartial", viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return RedirectToAction("Index", "Cart", new { CustomerId = customerId });
        }
    }
}
