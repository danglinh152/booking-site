using System.Diagnostics;
using System.Security.Cryptography;
using System.Text;
using BookingSite.Models;
using BookingSite.ViewModel;
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
    public IActionResult RegisterPage()
    {
      return View("Register");
    }

    //POST: /register
    [HttpPost("/register")]
    public async Task<IActionResult> Register(RegisterModelView registerModelView)
    {
      if (ModelState.IsValid)
      {
        //check email exist
        if (context.Users.Any(u => u.Email == registerModelView.Email))
        {
          ModelState.AddModelError("Email", "Email này đã được đăng ký!");
          return View(registerModelView);
        }

        // hashing password
        string hashedPassword = HashPassword(registerModelView.Password);

        string fullName = $"{registerModelView.FirstName} {registerModelView.LastName}";

        User newUser = new User
        {
          FullName = fullName,
          Email = registerModelView.Email,
          Password = hashedPassword,
          Role = "Client"
        };
        //save to DB
        await context.Users.AddAsync(newUser);
        int rows = await context.SaveChangesAsync();
        if (rows == 0) Console.WriteLine("Lỗi ! không thêm được");
        else Console.WriteLine($"Đã thêm {rows} user");
        return RedirectToAction("Index", "Admin");
      }
      var returnUrl = HttpContext.Session.GetString("ReturnUrl");
      if (!string.IsNullOrEmpty(returnUrl))
      {
        HttpContext.Session.Remove("ReturnUrl");
        return Redirect(returnUrl);
      }
      return Redirect("/login");
    }

    //GET: /login
    [HttpGet("/login")]
    public IActionResult LoginPage()
    {
      var session = HttpContext.Session;
      var userId = session.GetString("UserID");

      if (!string.IsNullOrEmpty(userId))
      {
        var returnUrl = HttpContext.Session.GetString("ReturnUrl");
        if (!string.IsNullOrEmpty(returnUrl))
        {
          HttpContext.Session.Remove("ReturnUrl");
          return Redirect(returnUrl);
        }
      }

      return View("Login");
    }

    //POST: /login
    [HttpPost("/login")]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
      string hashPassword = HashPassword(loginViewModel.Password);
      if (ModelState.IsValid)
      {
        User user = context.Users.FirstOrDefault(u => u.Email == loginViewModel.Email && u.Password == hashPassword);
        if (user == null)
        {
          ModelState.AddModelError(string.Empty, "Email hoặc mật khẩu không đúng");
          return View("Login", loginViewModel);
        }
        HttpContext.Session.SetString("UserID", user.UserID.ToString());
        HttpContext.Session.SetString("UserName", user.FullName.ToString());
        HttpContext.Session.SetString("UserRole", user.Role.ToString());

        // Kiểm tra có URL return không
        var returnUrl = HttpContext.Session.GetString("ReturnUrl");
        if (!string.IsNullOrEmpty(returnUrl))
        {
          HttpContext.Session.Remove("ReturnUrl");
          return Redirect(returnUrl);
        }

        if (user.Role == "Admin")
          return Redirect("/admin");
        else
          return Redirect("/");
      }
      return View("Login", loginViewModel);
    }
    [HttpGet("/logout")]
    public IActionResult Logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Login", "Auth");
    }

    public static string HashPassword(string password)
    {
      using (SHA256 sha256 = SHA256.Create())
      {
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
        return Convert.ToBase64String(bytes);
      }
    }
  }
}
