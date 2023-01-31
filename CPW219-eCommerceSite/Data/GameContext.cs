using CPW219_eCommerceSite.Models;
using Microsoft.EntityFrameworkCore;

namespace CPW219_eCommerceSite.Data
{
    public class GameContext: DbContext
    {
        public GameContext(DbContextOptions<GameContext> options) : base(options) 
        {
            
        }

        public DbSet<Game> Games { get; set; }


    }
}
