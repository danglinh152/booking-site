using System.Diagnostics;
using BookingSite.Models;
using BookingSite.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace BookingSite.Controllers
{
  public class AuthController(ILogger<AuthController> logger) : Controller
  {
    private readonly ILogger<AuthController> _logger = logger;

    //GET: /register
    [HttpGet("/register")]
    public IActionResult Index()
    {
      return View("Register");
    }

    //POST: /register
    [HttpPost("/register")]
    public IActionResult Register(RegiserModelView regiserModelView)
    {
      if (regiserModelView != null) {
        User user = new User();
        user.FullName = regiserModelView.FirstName + regiserModelView.LastName;
        user.Email = regiserModelView.Email;
      


      }
      return null

    }


  }
}
