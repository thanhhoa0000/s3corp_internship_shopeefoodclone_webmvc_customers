namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "Enter your username")]
    public string? UserName { get; set; }
    [PasswordPropertyText]
    [Required(ErrorMessage = "Enter your passwoord")]
    public string? Password { get; set; }
}
