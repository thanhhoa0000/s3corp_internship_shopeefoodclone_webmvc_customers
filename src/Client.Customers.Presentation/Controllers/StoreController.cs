namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class StoreController : Controller
{
    private readonly IStoreService _storeService;
    private readonly ICartService _cartService;
    private readonly IProductService _productService;
    private readonly IMenuService _menuService;
    private readonly ILogger<StoreController> _logger;

    public StoreController(
        IStoreService storeService,
        ICartService cartService,
        IProductService productService,
        IMenuService menuService,
        ILogger<StoreController> logger)
    {
        _storeService = storeService;
        _cartService = cartService;
        _productService = productService;
        _menuService = menuService;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult List() => View(new StorePromotionsViewModel());

    [HttpPost]
    public async Task<IActionResult> List(
        string province,
        string districtsString,
        string categoryName,
        string subcategoriesString,
        int pageSize = 15,
        int pageNumber = 1)
    {
        try
        {
            var stores = new List<StoreDto>();
            var storesCount = 0;
            var districts = districtsString?.Split(",").ToList() ?? new List<string>();
            var subcategories = subcategoriesString?.Split(",").ToList() ?? new List<string>();

            Response? storesCountResponse = await _storeService.GetStoresCount(new GetStoresCountRequest
            {
                LocationRequest = new LocationRequest
                {
                    Province = province,
                    Districts = districts
                },
                CategoryName = categoryName,
                SubCategoryNames = subcategories,
                IsPromoted = false
            });

            if (storesCountResponse!.IsSuccessful)
                storesCount = JsonSerializer.Deserialize<int>(
                    Convert.ToString(storesCountResponse.Body)!,
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
                    SubCategoryNames = subcategories,
                    PageSize = pageSize,
                    PageNumber = pageNumber
                });

            if (storesResponse!.IsSuccessful)
                stores = JsonSerializer.Deserialize<List<StoreDto>>(
                    Convert.ToString(storesResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var viewModel = new StorePromotionsViewModel
            {
                Stores = stores.ToList(),
                PagesCount = (int)Math.Ceiling((double)storesCount / pageSize),
                CurrentPage = pageNumber,
                TotalStoresCount = storesCount
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return View(new StorePromotionsViewModel());
        }
    }

    [HttpGet]
    public IActionResult Promotions() => View(new StorePromotionsViewModel());

    [HttpPost]
    public async Task<IActionResult> Promotions(
        string province,
        string districtsString,
        string categoryName,
        string subcategoriesString,
        int pageSize = 15,
        int pageNumber = 1)
    {
        try
        {
            var stores = new List<StoreDto>();
            var storesCount = 0;
            var districts = districtsString?.Split(",").ToList() ?? new List<string>();
            var subcategories = subcategoriesString?.Split(",").ToList() ?? new List<string>();

            Response? storesCountResponse = await _storeService.GetStoresCount(new GetStoresCountRequest
            {
                LocationRequest = new LocationRequest
                {
                    Province = province,
                    Districts = districts
                },
                CategoryName = categoryName,
                SubCategoryNames = subcategories,
                IsPromoted = false
            });

            if (storesCountResponse!.IsSuccessful)
                storesCount = JsonSerializer.Deserialize<int>(
                    Convert.ToString(storesCountResponse.Body)!,
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
                    SubCategoryNames = subcategories,
                    PageSize = pageSize,
                    PageNumber = pageNumber
                });

            if (storesResponse!.IsSuccessful)
                stores = JsonSerializer.Deserialize<List<StoreDto>>(
                    Convert.ToString(storesResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var viewModel = new StorePromotionsViewModel
            {
                Stores = stores.Where(store => store.IsPromoted).ToList(),
                PagesCount = (int)Math.Ceiling((double)storesCount / pageSize),
                CurrentPage = pageNumber,
                TotalStoresCount = storesCount,
            };

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return View(new StorePromotionsViewModel());
        }
    }

    [HttpGet]
    public async Task<IActionResult> Details(Guid storeId)
    {
        try
        {
            var store = new StoreDto();
            var products = new List<ProductDto>();
            var menuItems = new List<MenuDto>();

            _logger.LogDebug($"storeId: {storeId.ToString()}");

            Response? storeResponse = await _storeService.GetStoreDetails(storeId);

            if (storeResponse!.IsSuccessful && storeResponse.Body is not null)
                store = JsonSerializer.Deserialize<StoreDto>(
                    Convert.ToString(storeResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
            else
            {
                TempData["error"] = "Error occured when getting store details";

                return RedirectToAction("Index", "Home");
            }

            Response? productsResponse = await _productService.GetProductsByStoreIdAsync(
                request: new GetProductsRequest
                {
                    StoreId = storeId
                });

            if (productsResponse!.IsSuccessful)
                products = JsonSerializer.Deserialize<List<ProductDto>>(
                    Convert.ToString(productsResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            if (products.Count == 0)
            {
                TempData["error"] = "Store has no products";

                return RedirectToAction("Index", "Home");
            }

            Response? menuItemsResponse = await _menuService.GetMenuItemsByStoreIdAsync(
                request: new GetMenusRequest
                {
                    StoreId = store.Id
                });

            if (menuItemsResponse!.IsSuccessful)
                menuItems = JsonSerializer.Deserialize<List<MenuDto>>(
                    Convert.ToString(menuItemsResponse.Body)!,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

            var viewModel = new StoreDetailsViewModel
            {
                Store = store,
                Products = products,
                MenuItems = menuItems.Where(i => i.Products.Any()).ToList(),
                StarHtml = GenerateStarsHtml(products.Average(p => p.Rating))
            };

            if (User.Identity!.IsAuthenticated)
            {
                var customerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var cart = new CartDto();

                Response? cartResponse = await _cartService.GetCartAsync(Guid.Parse(customerId!));

                if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
                    cart = JsonSerializer.Deserialize<CartDto>(
                        Convert.ToString(cartResponse.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                viewModel.Cart = cart;
            }

            return View(viewModel);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex.ToString()}");
            TempData["error"] = "Đã xảy ra lỗi!";

            return View(new StoreDetailsViewModel());
        }
    }

    [HttpGet]
    public async Task<IActionResult> GetCartPartial(Guid customerId)
    {
        var cart = new CartDto();
        var cartResponse = await _cartService.GetCartAsync(customerId);

        if (cartResponse!.IsSuccessful && cartResponse.Body is not null)
            cart = JsonSerializer.Deserialize<CartDto>(
                Convert.ToString(cartResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

        return PartialView("_CartPartial", cart);
    }

    private string GenerateStarsHtml(double rating)
    {
        int fullStars = (int)rating;
        bool hasHalfStar = (rating - fullStars) >= 0.5;
        int emptyStars = 5 - fullStars - (hasHalfStar ? 1 : 0);

        StringBuilder html = new StringBuilder();
        html.Append("<div class='stars'>");

        for (int i = 0; i < fullStars; i++)
        {
            html.Append("<span><img class='' alt='' src='/images/star-full.png'/></span>");
        }

        if (hasHalfStar)
        {
            html.Append("<span><img class='' alt='' src='/images/star-half.png'/></span>");
        }

        for (int i = 0; i < emptyStars; i++)
        {
            html.Append("<span><img class='' alt='' src='/images/no-star.png'/></span>");
        }

        html.Append("</div>");
        return html.ToString();
    }
}