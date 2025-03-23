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
    // public IActionResult Register(RegiserModelView regiserModelView)
    // {

    // }


  }
}
