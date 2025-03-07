namespace ShopeeFoodClone.WebMvc.Customers.Presentation.Controllers
{
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
            Response? response = await _service.LoginAsync(model);
            
            if (response!.Body is not null && response.IsSuccessful)
            {
                LoginResponse loginResponse 
                    = JsonSerializer.Deserialize<LoginResponse>(
                        Convert.ToString(response.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(loginResponse.AccessToken);

                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "Role")?.Value ?? "";
                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "name")?.Value ?? "";

                if (roleClaim.IsNullOrEmpty())
                {
                    TempData["error"] = "Error(s) occurred!";

                    return View(model);
                }

                await SignUserIn(loginResponse, nameClaim);

                _tokenHandler.SetTokens(loginResponse.AccessToken, loginResponse.RefreshToken);

                if (roleClaim == "Customer")
                {
                    TempData["success"] = $"Hello {nameClaim}";

                    return RedirectToAction("Index", "Home");
                }

                TempData["error"] = $"Invalid login response from server: {response.Message}";

                return View(model);
            }
            if (response.Message.Contains("Username or password is incorrect!"))
            {
                TempData["error"] = response.Message;
            }
            else
            {
                TempData["error"] = $"Invalid login response from server: {response.Message}";
            }

            return View(model);
        }
        
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register() => View();
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            Response? response = await _service.RegisterAsync(model);

            if (response is not null && response.IsSuccessful)
            {
                if (model.Role.ToString().IsNullOrEmpty())
                    model.Role = Role.Customer;

                TempData["success"] = "Registering successfully!";

                return RedirectToAction("Login", "Account");
            }

            TempData["error"] = response!.Message;

            return View(model);
        }
        
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenHandler.ClearTokens();

            return RedirectToAction("Index", "Home");
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
}
