global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Net;
global using System.Text;
global using System.Text.Json;

global using Microsoft.AspNetCore.Http;
global using Microsoft.Extensions.DependencyInjection;

global using NLog;
global using NLog.Web;

global using ShopeeFoodClone.WebMvc.Customers.Application.Interfaces;
global using ShopeeFoodClone.WebMvc.Customers.Application.Services;
global using ShopeeFoodClone.WebMvc.Customers.Application.Configurations;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Dtos;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Requests;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Responses;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;
global using ShopeeFoodClone.WebMvc.Customers.Application.Models.Enums;