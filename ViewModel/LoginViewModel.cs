using System.ComponentModel.DataAnnotations;

namespace BookingSite.ViewModel
{
  public class LoginViewModel
  {
    [Required(ErrorMessage = "Email không được bỏ trống")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Mật khẩu không được bỏ trống")]
    [DataType(DataType.Password)]
    public string Password { get; set; }
  }
}
