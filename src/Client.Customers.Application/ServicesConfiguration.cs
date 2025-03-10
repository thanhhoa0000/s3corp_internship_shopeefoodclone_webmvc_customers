﻿namespace ShopeeFoodClone.WebMvc.Customers.Application;

public static partial class ServicesConfiguration
{
    public static IServiceCollection AddScopedServices(this IServiceCollection services)
    {
        services.AddScoped<IBaseService, BaseService>();
        services.AddScoped<ITokenProcessor, TokenProcessor>();
        services.AddScoped<IAccountService, AccountService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        return services;
    }

    public static IServiceCollection AddHttpClientServices(this IServiceCollection services)
    {
        services.AddHttpClient<ITokenProcessor, TokenProcessor>();
        services.AddHttpClient<IAccountService, AccountService>();
        services.AddHttpClient<ICategoryService, CategoryService>();
        
        return services;
    }
}