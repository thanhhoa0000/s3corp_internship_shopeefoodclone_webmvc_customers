namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class ConfigurationController : Controller
{
    private readonly ILogger<ConfigurationController> _logger;
    private readonly IConfiguration _configuration;

    public ConfigurationController(
        ILogger<ConfigurationController> logger,
        IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }
    
    public JsonResult Get()
    {
        var configuration = new
        {
            GatewayUrl = _configuration.GetSection("GatewayUrlForJS").Value,
        };
        return Json(configuration);
    }
}
