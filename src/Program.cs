var apiName = "[Customers] Web MVC";

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug($"Initializing {apiName}...\n-----\n");

try 
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    // Use NLog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();
    
    
    builder.Services.AddControllersWithViews();
    builder.Services.AddHttpContextAccessor();
    
    builder.Services
        .AddHttpClient("ShopeeFoodClone")
        .ConfigurePrimaryHttpMessageHandler(() =>
        {
            return new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
            };
        });
    
    builder.Services.AddHttpClient<ITokenProcessor, TokenProcessor>();
    builder.Services.AddHttpClient<IAccountService, AccountService>();
    
    ApiUrlProperties.ApiGatewayUrl = builder.Configuration["GatewayUrl"];
    
    builder.Services.AddScoped<IBaseService, BaseService>();
    builder.Services.AddScoped<ITokenProcessor, TokenProcessor>();
    builder.Services.AddScoped<IAccountService,  AccountService>();
    
    builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
        .AddCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(15);
            options.LoginPath = "/Account/Login";
            options.AccessDeniedPath = "/Account/AccessDenied";
        });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
    
    app.UseAuthentication();

    app.UseAuthorization();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}
catch (Exception ex)
{
    logger.Error($"Error(s) occured when starting {apiName}:\n-----\n{ex}");
}
finally
{
    LogManager.Shutdown();
}