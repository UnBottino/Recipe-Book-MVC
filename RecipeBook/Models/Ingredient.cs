using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RecipeBook.Models
{
    public class Ingredient
    {
        public Ingredient()
        : this(string.Empty)
        {
        }

        public Ingredient(string name)
            : this(name, 0, string.Empty)
        {
        }

        public Ingredient(string name, double amount, string unit)
            : this(null, name, amount, unit)
        {
        }

        public Ingredient(int? id, string name, double amount, string unit)
        {
            Id = id;
            Name = name;
            Amount = amount;
            Unit = unit;
        }

        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Amount { get; set; }
        [Required]
        public string Unit { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
