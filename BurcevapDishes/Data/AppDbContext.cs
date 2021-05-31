using BurcevapDishes.Models;
using Microsoft.EntityFrameworkCore;

namespace BurcevapDishes.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}
