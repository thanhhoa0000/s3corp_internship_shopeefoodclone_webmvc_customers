namespace ShopeeFoodClone.WebMvc.Customers.Application;

public static partial class ServicesConfiguration
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ITokenProcessor, TokenProcessor>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<ISubCategoryService, SubCategoryService>();
        services.AddScoped<IStoreService, StoreService>();
        
        return services;
    }

    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ITokenProcessor, TokenProcessor>();
        services.AddHttpClient<IAccountService, AccountService>();
        services.AddHttpClient<ICategoryService, CategoryService>();
        services.AddHttpClient<ISubCategoryService, SubCategoryService>();
        services.AddHttpClient<IStoreService, StoreService>();
        
        return services;
    }
}