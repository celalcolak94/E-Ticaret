using E_Ticaret.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Data
{
    public class DataContext : DbContext
    {
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderLine> OrderLines { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=ETicaret;Trusted_Connection=True;MultipleActiveResultSets=true;");

            base.OnConfiguring(optionsBuilder);
        }
    }
}
