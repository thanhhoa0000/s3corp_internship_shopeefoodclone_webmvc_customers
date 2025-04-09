namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class LoginViewModel
{
    [Required(ErrorMessage = "Tên đăng nhập không được để trống")]
    public string? UserName { get; set; }
    [PasswordPropertyText]
    [Required(ErrorMessage = "Mật khẩu không được để trống")]
    public string? Password { get; set; }
}
