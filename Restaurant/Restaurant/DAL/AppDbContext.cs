using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

    }
}
