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
        public DbSet<PriceRecord> PriceRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PriceRecord>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(pr =>  pr.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}