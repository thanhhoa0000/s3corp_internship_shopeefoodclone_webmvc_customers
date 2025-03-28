namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class ProductController : Controller
{
    private readonly IProductService _productService;
    private readonly ILogger<ProductController> _logger;

    public ProductController(
        IProductService productService,
        ILogger<ProductController> logger)
    {
        _productService = productService;
        _logger = logger;
    }
        
    public async Task<ActionResult> Details(Guid productId)
    {
        var product = new ProductDto();
        
        Response? productResponse = await _productService.GetProductByIdAsync(productId);
        
        if (productResponse!.IsSuccessful && productResponse.Body is not null)
            product = JsonSerializer.Deserialize<ProductDto>(
                Convert.ToString(productResponse.Body)!,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;
        else 
        {
            TempData["error"] = "Error occured when getting store details";

            return RedirectToAction("Index", "Home");
        }

        var viewModel = new ProductDetailsViewModel
        {
            Product = product,
            StartHtml = GenerateStarsHtml(product.Rating)
        };

        return View(viewModel);
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
            html.Append("<span><img class='' alt='' src='~/images/star-full.png'/></span>");
        }
        
        if (hasHalfStar)
        {
            html.Append("<span><img class='' alt='' src='~/images/star-half.png'/></span>");
        }
        
        for (int i = 0; i < emptyStars; i++)
        {
            html.Append("<span><img class='' alt='' src='~/images/no-star.png'/></span>");
        }

        html.Append("</div>");
        return html.ToString();
    }
}
