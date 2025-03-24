using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
  public class AuthController : Controller
  {
    private readonly FlightBookingContext context;

    public AuthController(FlightBookingContext context)
    {
      this.context = context;
    }

    //GET: /register
    [HttpGet("/register")]
    public IActionResult Index()
    {
      return View("Register");
    }

    //POST: /register
    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegiserModelView regiserModelView)
    {
      if (ModelState.IsValid)
      {
        //check email exist
        if (context.Users.Any(u => u.Email == regiserModelView.Email))
        {
          ModelState.AddModelError("Email", "Email này đã được đăng ký!");
          return View(regiserModelView);
        }

        // hashing password
        string hashedPassword = HashPassword(regiserModelView.Password);

        string fullName = $"{regiserModelView.FirstName} {regiserModelView.LastName}";

        User newUser = new User
        {
          FullName = fullName,
          Email = regiserModelView.Email,
          Password = hashedPassword,
          Role = "Client"
        };
        //save to DB
        await context.Users.AddAsync(newUser);
        int rows = await context.SaveChangesAsync();
        if (rows == 0) Console.WriteLine("Lỗi ! không thêm được");
        else Console.WriteLine($"Đã thêm {rows} user");
        return RedirectToAction("");
      }
      return View(regiserModelView);
    }
    private string HashPassword(string password)
    {
      using (SHA256 sha256 = SHA256.Create())
      {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
      }
    }
  }
}
