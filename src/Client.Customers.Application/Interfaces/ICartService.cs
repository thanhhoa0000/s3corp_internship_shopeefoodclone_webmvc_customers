﻿namespace ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

public interface ICartService
{
    Task<Response?> GetCartAsync(Guid customerId);
    Task<Response?> AddToCartAsync(AddToCartRequest request);
}