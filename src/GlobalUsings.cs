global using System.Net;
global using System.Text;
global using System.Text.Json;
global using System.Diagnostics;
global using System.ComponentModel;
global using System.ComponentModel.DataAnnotations;
global using System.IdentityModel.Tokens.Jwt;
global using System.Security.Cryptography.X509Certificates;
global using System.Security.Claims;

global using Microsoft.AspNetCore.DataProtection;
global using Microsoft.AspNetCore.Authentication.Cookies;
global using Microsoft.AspNetCore.Authentication;
global using Microsoft.IdentityModel.Tokens;
global using Microsoft.AspNetCore.Mvc;
global using Microsoft.AspNetCore.Authorization;
global using Microsoft.AspNetCore.Identity;

global using NLog;
global using NLog.Web;

global using ShopeeFoodClone.WebMvc.Customers.Models;
global using ShopeeFoodClone.WebMvc.Customers.Models.Enums;
global using ShopeeFoodClone.WebMvc.Customers.Interfaces;
global using ShopeeFoodClone.WebMvc.Customers.Services;
global using ShopeeFoodClone.WebMvc.Customers.Configurations;