namespace ShopeeFoodClone.WebMvc.Customers.Models;

public class LoginViewModel
{
    [Required(ErrorMessage = "Enter your username")]
    [MinLength(6, ErrorMessage = "Username must be at least 6 characters")]
    public string? UserName { get; set; }
    [PasswordPropertyText]
    [Required(ErrorMessage = "Enter your passwoord")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters")]
    public string? Password { get; set; }
}