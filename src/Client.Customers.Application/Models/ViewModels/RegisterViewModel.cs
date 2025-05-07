namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class RegisterViewModel
{
    [Required(ErrorMessage = "Vui lòng nhập tên người dùng của bạn")]
    [MinLength(6, ErrorMessage = "Tên đăng nhập phải chứa ít nhất 6 ký tự")]
    public string? UserName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string? Email { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [MinLength(8, ErrorMessage = "Mật khẩu phải chứa ít nhất 8 ký tự")]
    [RegularExpression(@"^[^\s]+$", ErrorMessage = "Mật khẩu không được có khoảng trống")]
    [DataType(DataType.Password)]
    public string? Password { get; set; }
    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mật khẩu không trùng khớp")]
    public string? ConfirmPassword { get; set; }
    [RegularExpression(@"^(84|0[3|5|7|8|9])[0-9]{8}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    [Required(ErrorMessage = "Vui lòng nhập số điện thoại của bạn")]
    public string? PhoneNumber { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập tên của bạn")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
    [MinLength(2, ErrorMessage = "Tên phải chứa ít nhất 2 ký tự"), MaxLength(30, ErrorMessage = "Tên không được quá 30 ký tự")]
    public string? FirstName { get; set; }
    [Required(ErrorMessage = "Vui lòng nhập họ của bạn")]
    [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "Tên không được chứa ký tự đặc biệt")]
    [MinLength(2, ErrorMessage = "Tên phải chứa ít nhất 2 ký tự"), MaxLength(50, ErrorMessage = "Tên không được quá 50 ký tự")]
    public string? LastName { get; set; }

    public Role Role { get; set; } = Role.Customer;
}
