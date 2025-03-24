using Microsoft.AspNetCore.Mvc;
using BookingSite.Models; // Ensure this points to the correct location of your Meal model

namespace BookingSite.Controllers
{
    [Route("admin/[controller]")]
    public class MealsController : Controller
    {
        // GET: admin/meals
        [HttpGet]
        public IActionResult Meals()
        {
            return View();
        }

        /* ==============================================================================================*/
        // GET: admin/meals/create
        [HttpGet("create")]
        public IActionResult CreateMeal()
        {
            return View();
        }

        /* ==============================================================================================*/


        /* ==============================================================================================*/
        // GET: admin/meals/edit/1
        [HttpGet("edit/{id}")]
        public IActionResult EditMeal(int id)
        {
            // Logic to get meal details by id
            var meal = GetMealById(id); // Replace with your actual data retrieval logic

            if (meal == null)
            {
                return NotFound(); // Return a 404 if not found
            }
            return View(meal);
        }

        private Meal GetMealById(int id)
        {
            // Replace with your actual data source
            return new Meal
            {

            };
        }
        /* ==============================================================================================*/
    }
}
