global using System.Text.Json;
global using System.Diagnostics;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using Microsoft.AspNetCore.DataProtection;

global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;

global using NLog;
global using NLog.Web;

global using ShopeeFoodClone.WebMvc.Customers.Application;
global using ShopeeFoodClone.WebMvc.Customers.Application.Configurations;
global using ShopeeFoodClone.WebMvc.Customers.Application.Dtos;
global using ShopeeFoodClone.WebMvc.Customers.Application.Dtos.Enums;
global using ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;
global using ShopeeFoodClone.WebMvc.Customers.Application.Services;
