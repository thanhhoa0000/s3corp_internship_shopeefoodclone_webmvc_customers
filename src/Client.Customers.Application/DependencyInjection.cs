namespace ShopeeFoodClone.WebMvc.Customers.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ITokenProcessor, TokenProcessor>();
        services.AddScoped<IAccountService,  AccountService>();
        
        return services;
    }
}