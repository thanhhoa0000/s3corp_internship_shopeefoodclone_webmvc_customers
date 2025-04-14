namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người dùng của bạn")]
    [MinLength(6, ErrorMessage = "Tên đăng nhập phải chứa ít nhất 6 ký tự")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Invalid email address")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(8, ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
    public string? ConfirmPassword { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn")]
    [Phone(ErrorMessage = "Số điện thoại không hợp lệ"), MaxLength(12)]
    public string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
    [MinLength(2, ErrorMessage = "Tên phải chứa ít nhất 2 ký tự"), MaxLength(30)]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ của bạn")]
    [MinLength(2, ErrorMessage = "Tên phải chứa ít nhất 2 ký tự"), MaxLength(50)]
    public string? LastName { get; set; }

    public Role Role { get; set; } = Role.Customer;
}
