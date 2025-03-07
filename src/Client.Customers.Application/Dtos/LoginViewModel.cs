namespace ShopeeFoodClone.WebMvc.Customers.Application.Dtos;

public class LoginViewModel
{
    [Required(ErrorMessage = "Enter your username")]
    public string? UserName { get; set; }
    [PasswordPropertyText]
    [Required(ErrorMessage = "Enter your passwoord")]
    public string? Password { get; set; }
}