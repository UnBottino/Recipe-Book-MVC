using System.ComponentModel.DataAnnotations;

namespace RecipeBook.Models
{
    public class Direction
    {
        public Direction()
        : this(string.Empty)
        {
        }

        public Direction(string directionText)
            : this(0, directionText)
        {
        }

        public Direction(int stepNumber, string directionText)
            : this(null, stepNumber, directionText)
        {
        }

        public Direction(int? id, int stepNumber, string directionText)
        {
            Id = id;
            StepNumber = stepNumber;
            DirectionText = directionText;
        }

        public int? Id { get; set; }
        [Required]
        public int StepNumber { get; set; }
        [Required]
        public string DirectionText { get; set; }
        [Required]
        public int RecipeId { get; set; }
    }
}
