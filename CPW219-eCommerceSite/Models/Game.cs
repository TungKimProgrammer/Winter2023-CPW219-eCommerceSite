using System.ComponentModel.DataAnnotations;

namespace CPW219_eCommerceSite.Models
{
    /// <summary>
    /// Represents a single Game for purchase
    /// </summary>
    public class Game
    {
        /// <summary>
        /// The unique identifier for each game product
        /// </summary>
        [Key]
        public int GameId { get; set; }

        /// <summary>
        /// The official Title of Video Game
        /// </summary>
        [Required]
        public string Title{ get; set; }
        
        /// <summary>
        /// The sales price of the Game
        /// </summary>
        [Required]
        [Range(0, double.MaxValue)]
        public double Price { get; set; }

        

        // Todo: Add Rating
    }

    /// <summary>
    /// A single video game that has been add to the users shopping cart cookie
    /// </summary>
    public class CartGameViewModel
    {
        public int GameId { get; set; }

        public string Title { get; set; }

        public double Price { get; set; }
    }
}
