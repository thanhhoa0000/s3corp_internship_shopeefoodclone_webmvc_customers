﻿namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class RegisterViewModel
{
    [Required(ErrorMessage = "Enter your username")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Enter your email")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Enter your passwoord")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Confirm your password")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    public string? ConfirmPassword { get; set; }
    [Phone(ErrorMessage = "Invalid phone number format"), MaxLength(12)]
    public string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "Enter your first name")]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters"), MaxLength(30)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Enter your last name")]
    [MinLength(2, ErrorMessage = "Must be at least 2 characters"), MaxLength(50)]
    public string? LastName { get; set; }
    public Role Role { get; set; }
}
