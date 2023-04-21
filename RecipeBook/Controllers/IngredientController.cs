using Microsoft.AspNetCore.Mvc;

namespace RecipeBook.Controllers
{
    public class IngredientController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
