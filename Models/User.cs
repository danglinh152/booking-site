using System.ComponentModel.DataAnnotations;

namespace BookingSite.Models
{
  public class User
  {

    public int UserId { get; set; }

    public string FullName { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập email")]
    [EmailAddress(ErrorMessage = "Email không hợp lệ")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
    [DataType(DataType.Password)]
    public string Password { get; set; }

    public string Role { get; set; } = "Client";

  }
}
