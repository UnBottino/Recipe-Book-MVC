using System.ComponentModel.DataAnnotations;
namespace RecipeBook.Models
{
    public enum RecipeType
    {
        Cooking, Baking, Drink
    }

    public class Recipe
    {
        public Recipe()
            : this(string.Empty, RecipeType.Baking, string.Empty, string.Empty, new List<Ingredient>(), new List<Direction>())
        {
        }

        public Recipe(string title, RecipeType type, string description, string imageUrl, List<Ingredient> ingredients, List<Direction> directions)
            :this(null, title, type, description, imageUrl, ingredients, directions)
        {
        }

        public Recipe(int? id, string title, RecipeType type, string description, string imageUrl, List<Ingredient> ingredients, List<Direction> directions)
        {
            Id = id;
            Title = title;
            Type = type;
            Description = description;
            ImageUrl = imageUrl;
            Ingredients = ingredients;
            Directions = directions;
        }

        public int? Id { get; set; }

        [Required(ErrorMessage = "Title is required!")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Type is required!")]
        public RecipeType Type { get; set; }
        [Required(ErrorMessage = "A discription is required!")]
        public string Description { get; set; }
        [Required(ErrorMessage = "A url is required!")]
        public string ImageUrl { get; set; }
        [Required(ErrorMessage = "Ingredients are required!")]
        public List<Ingredient> Ingredients { get; set; }
        [Required(ErrorMessage = "Directions are required!")]
        public List<Direction> Directions { get; set; }
    }
}
