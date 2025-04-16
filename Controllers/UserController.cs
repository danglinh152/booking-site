using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your User model

namespace BookingSite.Controllers
{
    [AuthorizeRole("Admin")]
    [Route("admin/users")]
    public class UsersController : Controller
    {
        private FlightBookingContext context;

        public UsersController(FlightBookingContext context)
        {
            this.context = context;
        }

        // GET: admin/users 
        [HttpGet]
        public IActionResult Users()
        {
            var users = context.Users.ToList();
            return View(users);
        }

        // GET: admin/users/create
        [HttpGet("create")]
        public IActionResult CreateUser()
        {
            return View();
        }
        // POST: admin/users/create
        [HttpPost("create")]
        public IActionResult CreateUser(User user)
        {

            if (ModelState.IsValid)
            {
                context.Users.Add(user);
                context.SaveChanges();
                return RedirectToAction("Users");
            }
            return View(user);
        }

        // GET: admin/users/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditUser(int id)
        {
            // Logic to get user details by id
            var user = context.Users.Find(id); // Replace with your actual data retrieval logic
            if (user == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(user);
        }

        // POST: admin/users/edit/1
        [HttpPost("edit/{id}")]
        public async Task<IActionResult> EditUser(int id, User updatedUser)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.FullName = updatedUser.FullName;
            user.Email = updatedUser.Email;
            user.Role = updatedUser.Role;

            try
            {
                await context.SaveChangesAsync();
                return RedirectToAction(nameof(Users));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[ERROR] {ex.Message}");
                ModelState.AddModelError("", "Có lỗi xảy ra khi cập nhật. Vui lòng thử lại.");
                return View(updatedUser);
            }
        }

        // DELETE: admin/users/delete/1
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = await context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            context.Users.Remove(user);
            await context.SaveChangesAsync();

            return RedirectToAction(nameof(Users));
        }

    }
}
