using RecipeBook.Models;

namespace RecipeBook.DAL
{
    public class DbInitializer
    {
        public static void Initialize(RecipeBookDbContext context)
        {
            context.Database.EnsureCreated();

            // Look for any recipes.
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }

            var recipe = new Recipe()
            {
                Title = "Pastry",
                Type = RecipeType.Baking,
                Description = "A Flat sheet of pastry",
                Ingredients = new List<Ingredient>()
                {
                    new Ingredient { Name="Flour", Amount=16, Unit="oz" },
                    new Ingredient { Name="Margarine", Amount=4, Unit="oz" },
                    new Ingredient { Name="Salt", Amount=1, Unit="pinch" },
                    new Ingredient { Name="Water", Amount=1, Unit="As needed" }
                },
                    Directions = new List<Direction>()
                {
                    new Direction { StepNumber=1, DirectionText="Have everything cold" },
                    new Direction { StepNumber=2, DirectionText="Sift flour and salt in a bowl" },
                    new Direction { StepNumber=3, DirectionText="Rub in margarine until like breadcrumbs" },
                    new Direction { StepNumber=4, DirectionText="Add water gradually and mix into a stiff paste" },
                    new Direction { StepNumber=5, DirectionText="Leave in the oven until golden brown" },
                }
            };

            context.Recipes.Add(recipe);
            context.SaveChanges();
        }
    }
}
