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
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICollectionService, CollectionService>();
        services.AddScoped<IMenuService, MenuService>();
        
        return services;
    }
}
