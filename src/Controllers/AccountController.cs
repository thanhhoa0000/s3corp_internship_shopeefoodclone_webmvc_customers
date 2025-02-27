using Microsoft.AspNetCore.Mvc;

namespace ShopeeFoodClone.WebMvc.Customers.Controllers
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
        public IActionResult Login()
        {
            LoginViewModel model = new();
            return View(model);
        }
        
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            Response? response = await _service.LoginAsync(model);

            _logger.LogDebug($"Account controller response: {((JsonElement)response!.Body!).GetRawText()}\n-----\n");
            
            if (response.Message.Contains("Username or password is wrong!"))
            {
                TempData["error"] = response.Message;
                
                return View(model);
            }

            if (!response.Body.ToString().IsNullOrEmpty() && response.IsSuccessful)
            {
                LoginResponse loginResponse 
                    = JsonSerializer.Deserialize<LoginResponse>(
                        Convert.ToString(response.Body)!,
                        new JsonSerializerOptions { PropertyNameCaseInsensitive = true })!;

                var handler = new JwtSecurityTokenHandler();
                var jwtToken = handler.ReadJwtToken(loginResponse.AccessToken);

                var roleClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role || c.Type == "role")?.Value ?? "";
                var nameClaim = jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Name || c.Type == "name")?.Value ?? "";

                if (roleClaim.IsNullOrEmpty())
                {
                    TempData["error"] = "Error(s) occured!";

                    return View(model);
                }

                await SignUserIn(loginResponse, nameClaim);

                _tokenHandler.SetTokens(loginResponse.AccessToken, loginResponse.RefreshToken);

                if (roleClaim != "Admin")
                {
                    TempData["success"] = $"Hello {nameClaim}";

                    return RedirectToAction("Index", "Home");
                }

                TempData["error"] = $"Invalid login response from server: {response.Message}";

                return View(model);
            }
            
            TempData["error"] = $"Invalid login response from server: {response.Message}";

            return View(model);
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
                jwtToken.Claims.FirstOrDefault(u => u.Type == ClaimTypes.Role)!.Value));

            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
