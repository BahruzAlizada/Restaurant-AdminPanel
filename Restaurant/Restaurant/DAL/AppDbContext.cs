using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Restaurant.Models;

namespace Restaurant.DAL
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<SpecialMenu> SpecialMenus { get; set; }

        public DbSet<Profit> Profits { get; set; }
        public DbSet<Cost> Costs { get; set; }
        public DbSet<Salary> Salarys { get; set; }
        public DbSet<Total> Totals { get; set; }

        public DbSet<Reservation> Reservations { get; set; } 
        public DbSet<Table> Tables { get; set; }

        public DbSet<Cash> Cashes { get; set; }
        public DbSet<CashProduct> CashProducts { get; set; }

        public DbSet<Kitchen> Kitchens { get; set; }
        public DbSet<KitchenBase> KitchenBases { get; set; }
    }
}
