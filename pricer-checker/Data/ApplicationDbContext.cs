using Microsoft.EntityFrameworkCore;
using pricer_checker.Models.Entities;

namespace pricer_checker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }
    }
}