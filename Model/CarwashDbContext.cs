using CarwashMonitor.Models;
using Microsoft.EntityFrameworkCore;

namespace CarwashMonitor.Model
{
    public class CarwashDbContext : DbContext
    {
        public CarwashDbContext (DbContextOptions<CarwashDbContext> options) : base (options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<Box> Boxes { get; set; }
    }
}
