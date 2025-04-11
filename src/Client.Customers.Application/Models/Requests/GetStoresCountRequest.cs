﻿namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;

public sealed class GetStoresCountRequest
{
    public LocationRequest? LocationRequest { get; set; }
    public bool IsPromoted { get; set; } = false;
}
