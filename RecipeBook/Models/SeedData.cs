using Microsoft.EntityFrameworkCore;
using RecipeBook.DAL;

namespace RecipeBook.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new RecipeBookDbContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<RecipeBookDbContext>>()))
        {
            // Look for any movies.
            if (context.Recipes.Any())
            {
                return;   // DB has been seeded
            }
            context.Recipes.AddRange(
                new Recipe
                {
                    Title = "Pastry",
                    Type = RecipeType.Baking,
                    Description = "This recipe is for a wonderful flakey pastry",
                    ImageUrl = "https://anitalianinmykitchen.com/wp-content/uploads/2018/11/puff-pastry-sq-500x500.jpg",
                    Ingredients = new List<Ingredient>
                    {
                        new Ingredient { Name = "Plain Flour", Amount = 16, Unit = "oz" },
                        new Ingredient { Name = "Margarine", Amount = 4, Unit = "oz" },
                        new Ingredient { Name = "Salt", Amount = 1, Unit = "Pinch" },
                        new Ingredient { Name = "Water", Amount = 1, Unit = "As needed" }
                    },
                    Directions = new List<Direction>
                    {
                        new Direction { StepNumber = 1, DirectionText = "Have everything cold"},
                        new Direction { StepNumber = 2, DirectionText = "Sift flour and salt in a bowl"},
                        new Direction { StepNumber = 3, DirectionText = "Rub in margarine until like breadcrumbs"},
                        new Direction { StepNumber = 4, DirectionText = "Add water gradually and mix into a stiff paste"},
                        new Direction { StepNumber = 5, DirectionText = "Leave in the oven until golden brown"}
                    }
                }
            );
            context.SaveChanges();
        }
    }
}
