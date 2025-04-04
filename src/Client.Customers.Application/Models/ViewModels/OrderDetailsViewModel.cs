namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.ViewModels;

public class OrderDetailsViewModel
{
    public CartDto? Cart { get; set; }
    public string? CustomerName { get; set; }
    [Required(ErrorMessage = "Số điện thoại không thể để trống")]
    public string? CustomerPhoneNumber { get; set; }
    [Required(ErrorMessage = "Địa chỉ không thể để trống")]
    public string? CustomerAddress { get; set; }
}
