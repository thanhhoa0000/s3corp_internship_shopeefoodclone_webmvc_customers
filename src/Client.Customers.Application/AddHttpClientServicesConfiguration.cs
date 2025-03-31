namespace ShopeeFoodClone.WebMvc.Customers.Application;

public static partial class ServicesConfiguration
{
    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ITokenProcessor, TokenProcessor>();
        services.AddHttpClient<IAccountService, AccountService>();
        services.AddHttpClient<ICategoryService, CategoryService>();
        services.AddHttpClient<ISubCategoryService, SubCategoryService>();
        services.AddHttpClient<IStoreService, StoreService>();
        services.AddHttpClient<IProductService, ProductService>();
        services.AddHttpClient<ICollectionService, CollectionService>();
        services.AddHttpClient<IMenuService, MenuService>();
        services.AddHttpClient<ICartService, CartService>();
        
        return services;
    }
}
