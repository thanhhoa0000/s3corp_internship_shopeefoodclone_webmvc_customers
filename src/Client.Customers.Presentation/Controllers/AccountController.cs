namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers;

public class AccountController : Controller
{
    private readonly IAccountService _service;
    private readonly ITokenProcessor _tokenHandler;
    private readonly ILogger<AccountController> _logger;

    public AccountController(IAccountService service, ITokenProcessor tokenHandler, ILogger<AccountController> logger)
    {
        _service = service;
        _tokenHandler = tokenHandler;
        _logger = logger;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Login() => View(new LoginViewModel());

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        try
        {
            Response? response = await _service.LoginAsync(model);

            if (response!.Body is not null && response.IsSuccessful)
            {
                LoginResponse loginResponse
                    = JsonSerializer.Deserialize<LoginResponse>(
                        Convert.ToString(response.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(loginResponse.AccessToken);

                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "Role")
                                    ?.Value ??
                                "";
                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "name")
                                    ?.Value ??
                                "";

                if (roleClaim.IsNullOrEmpty())
                {
                    TempData["error"] = "Đã xảy ra lỗi!";
                    _logger.LogDebug("flag");

                    return View(model);
                }

                await SignUserIn(loginResponse, nameClaim);

                _tokenHandler.SetTokens(loginResponse.AccessToken, loginResponse.RefreshToken);

                if (roleClaim == "Customer")
                {
                    TempData["success"] = $"Xin chào {nameClaim}";

                    return RedirectToAction("Index", "Home");
                }

                TempData["error"] = $"Đã xảy ra lỗi!";

                return View(model);
            }

            if (response.Message.Contains("Username or password is incorrect!"))
            {
                TempData["error"] = "Tên đăng nhập hoặc mật khẩu không đúng!";
            }
            else
            {
                TempData["error"] = $"Đã xảy ra lỗi!";
            }

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new LoginViewModel());
        }
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register() => View();

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model)
    {
        try
        {
            Response? response = await _service.RegisterAsync(model);

            if (response is not null && response.IsSuccessful)
            {
                if (model.Role.ToString().IsNullOrEmpty())
                    model.Role = Role.Customer;

                TempData["success"] = "Đăng ký tài khoản thành công!";

                return RedirectToAction("Login", "Account");
            }

            TempData["error"] = response!.Message;

            return View(model);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error occurred: {ex}");
            TempData["error"] = "Đã xảy ra lỗi!";
            
            return View(new RegisterViewModel());
        }
    }

    [AllowAnonymous]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync();
        _tokenHandler.ClearTokens();

        TempData["success"] = "Đăng xuất thành công!";

        return RedirectToAction("Index", "Home");
    }
    
    [HttpGet]
    public IActionResult IsUserAuthenticated()
    {
        if (User.Identity!.IsAuthenticated)
        {
            return Json(new { isAuthenticated = true });
        }

        TempData["error"] = "Vui lòng đăng nhập trước khi sử dụng dịch vụ!";
        return Json(new { isAuthenticated = false });
    }

    [Route("Account/dangnhap")]
    [Route("taikhoan/dangnhap")]
    [Route("Account/dang-nhap")]
    [Route("taikhoan/dang-nhap")]
    [Route("taikhoan/login")]
    public IActionResult RedirectToLogin()
    {
        return RedirectToRoutePermanent("Default", new { controller = "Account", action = "Login" });
    }
    
    [Route("Account/dangky")]
    [Route("taikhoan/dangky")]
    [Route("Account/dang-ky")]
    [Route("taikhoan/dang-ky")]
    [Route("taikhoan/register")]
    public IActionResult RedirectToRegister()
    {
        return RedirectToRoutePermanent("Default", new { controller = "Account", action = "Register" });
    }

    private async Task SignUserIn(LoginResponse response, string nameClaim)
    {
        var handler = new JwtSecurityTokenHandler();

        var jwtToken = handler.ReadJwtToken(response.AccessToken);

        var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)!.Value));
        identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name,
            jwtToken.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)!.Value));
        identity.AddClaim(new Claim(ClaimTypes.Name, nameClaim));
        identity.AddClaim(new Claim(ClaimTypes.Role,
            jwtToken.Claims.FirstOrDefault(u => u.Type == "Role")!.Value));

        var principal = new ClaimsPrincipal(identity);

        await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
    }
}
