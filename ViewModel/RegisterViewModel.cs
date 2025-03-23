using System.ComponentModel.DataAnnotations;

namespace BookingSite.ViewModel
{
  public class RegiserModelView
  {
    [Required(ErrorMessage = "Vui lòng nhập họ")]
    public string FirstName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập tên")]
    public string LastName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    [Required(ErrorMessage = "Vui lòng xác nhận mật khẩu")]
    [DataType(DataType.Password)]
    [Compare("Password", ErrorMessage = "Mật khẩu xác nhận không khớp")]
    public string ConfirmPassword { get; set; }
  }
}