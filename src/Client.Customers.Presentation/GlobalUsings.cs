global using System.Text;
global using System.Text.Json;
global using System.Diagnostics;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Claims;
global using System.Globalization;
global using System.ComponentModel.DataAnnotations;
global using System.Reflection;

global using Microsoft.AspNetCore.DataProtection;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Diagnostics;

global using NLog;
global using NLog.Web;

global using ShopeeFoodClone.WebMvc.Customers.Application;
global using ShopeeFoodClone.WebMvc.Customers.Application.Configurations;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Responses;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Enums;
global using ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;

global using ShopeeFoodClone.WebMvc.Customers.Presentation.Middlewares;
