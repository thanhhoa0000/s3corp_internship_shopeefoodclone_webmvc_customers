namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public sealed class OrderInitViewModel
{
    public CartDto? Cart { get; set; }
    public StoreDto? Store { get; set; }
    public string? CustomerName { get; set; }
    [Required(ErrorMessage = "Số điện thoại không thể để trống")]
    [RegularExpression(@"^\d{1,11}$", ErrorMessage = "Số điện thoại không hợp lệ")]
    public string? CustomerPhoneNumber { get; set; }
    [Required(ErrorMessage = "Địa chỉ không thể để trống")]
    public string? CustomerAddress { get; set; }
}
