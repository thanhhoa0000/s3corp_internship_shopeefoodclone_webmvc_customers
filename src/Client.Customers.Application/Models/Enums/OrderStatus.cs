namespace ShopeeFoodClone.WebMvc.Customers.Application.Models.Enums;
public enum OrderStatus : byte
{
    [Display(Name = "Đang chờ")]
    Pending,

    [Display(Name = "Đã giao")]
    Delivered,

    [Display(Name = "Đã hủy")]
    Canceled,

    [Display(Name = "Khách đã xóa")]
    DeletedByCustomer
}
