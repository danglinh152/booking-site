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
  }
}