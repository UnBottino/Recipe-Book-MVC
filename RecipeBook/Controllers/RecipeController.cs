using Microsoft.AspNetCore.Mvc;
using RecipeBook.DAL;
using RecipeBook.Models;
using System.Net;

namespace RecipeBook.Controllers
{
    public class RecipeController : Controller
    {
        private RecipeBookDbContext _dbContext;

        public RecipeController(RecipeBookDbContext db)
        {
            _dbContext = db;
        }

        // 
        // GET: /Recipe/ 
        public IActionResult Index()
        {
            IEnumerable<Recipe> recipeList = _dbContext.Recipes;
            return View(recipeList);
        }

        // 
        // GET: /Recipe/RecipeContainer
        public IActionResult Search(string term)
        {
            IEnumerable<Recipe> recipeList = _dbContext.Recipes.Where(x => x.Title.Contains(term, StringComparison.OrdinalIgnoreCase));
            return PartialView("~/Views/Recipe/RecipeContainer.cshtml", recipeList);
        }

        //GET
        public IActionResult Create()
        {
            return View(new Recipe());
        }

        //POST
        [HttpPost]
        public JsonResult Create(Recipe model)
        {
            model.Ingredients.RemoveAll(x => x.Name == null);
            model.Directions.RemoveAll(x => x.DirectionText == null);

            _dbContext.Recipes.Add(model);
            _dbContext.SaveChanges();

            return Json(new { Success = true, model.Title});
        }

        //GET
        [Route("Get/{id:int}")]
        public IActionResult Get(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

           var recipeFromDb = _dbContext.Recipes.FirstOrDefault(i => i.Id == id);

            if (recipeFromDb == null)
            {
                return NotFound();
            }
            else
            {
                var ingredientList = _dbContext.Ingredients.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
                var directionsList = _dbContext.Directions.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
            }

            return View(recipeFromDb);
        }

        //GET
        [Route("Get/{title}")]
        public IActionResult Get(string title)
        {
            if (string.IsNullOrEmpty(title))
            {
                return NotFound();
            }

            var recipeFromDb = _dbContext.Recipes.OrderByDescending(i => i.Id).FirstOrDefault(i => string.Equals(i.Title, title));

            if (recipeFromDb == null)
            {
                return NotFound();
            }
            else
            {
                var ingredientList = _dbContext.Ingredients.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
                var directionsList = _dbContext.Directions.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
            }

            return View(recipeFromDb);
        }

        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var recipeFromDb = _dbContext.Recipes.Find(id);

            if (recipeFromDb == null)
            {
                return NotFound();
            }
            else
            {
                var ingredientList = _dbContext.Ingredients.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
                var directionsList = _dbContext.Directions.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
            }

            return View(recipeFromDb);
        }

        //POST
        [HttpPost]
        public JsonResult Edit(Recipe model)
        {
            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            model.Ingredients.RemoveAll(x => x.Name == null);
            model.Directions.RemoveAll(x => x.DirectionText == null);

            try
            {
                _dbContext.RemoveRange(_dbContext.Ingredients.Where(x => x.RecipeId == model.Id));
                _dbContext.RemoveRange(_dbContext.Directions.Where(x => x.RecipeId == model.Id));

                var recipe = new Recipe()
                {
                    Id = model.Id,
                    Title = model.Title,
                    Type = model.Type,
                    Description = model.Description,
                    ImageUrl = model.ImageUrl,
                    Ingredients = model.Ingredients,
                    Directions = model.Directions
                };

                _dbContext.Recipes.Update(recipe);
                _dbContext.SaveChanges();

                Response.StatusCode = (int)HttpStatusCode.OK;
                return Json(new { Success = true, RedirectUrl = "/Recipe"});
            }
            catch(Exception ex)
            {
                return Json(new { Success = false, ex.Message});
            }
        }


        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var recipeFromDb = _dbContext.Recipes.Find(id);

            if (recipeFromDb == null)
            {
                return NotFound();
            }
            else
            {
                var ingredientList = _dbContext.Ingredients.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
                var directionsList = _dbContext.Directions.Where(i => i.RecipeId == recipeFromDb.Id).ToList();
            }

            _dbContext.Recipes.Remove(recipeFromDb);
            _dbContext.SaveChanges();

            return RedirectToAction("Index");
        }

        public IActionResult GetRecipes()
        {
            var recipeList = _dbContext.Recipes.ToList();

            foreach (var recipe in recipeList)
            {
                var ingredientList = _dbContext.Ingredients.Where(i => i.RecipeId == recipe.Id).ToList();
                var directionsList = _dbContext.Directions.Where(i => i.RecipeId == recipe.Id).ToList();
            }

            return View(recipeList);
        }
    }
}
