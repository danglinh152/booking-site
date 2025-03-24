using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your User model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class UsersController : Controller
    {
        // GET: admin/users
        [HttpGet]
        public IActionResult Users()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/users/create
        [HttpGet("create")]
        public IActionResult CreateUser()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/users/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditUser(int id)
        {
            // Logic to get user details by id
            var user = GetUserById(id); // Replace with your actual data retrieval logic

            if (user == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(user);
        }

        private User GetUserById(int id)
        {
            // Replace with your actual data source
            return new User
            {
                UserID = id,
                FullName = "Dang Linh",
                Email = "danglinh.k4@gmail.com",
                Password = "cc30cm"
            };
        }
        /* ==============================================================================================*/
    }
}
