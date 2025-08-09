using Microsoft.EntityFrameworkCore;

namespace SpazaStock.Server.Models
{
    public class SpazaStockDbContext : DbContext
    {
        public SpazaStockDbContext(DbContextOptions<SpazaStockDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleItem> SaleItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // SaleItem relationships
            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Sale)
                .WithMany(s => s.SaleItems)
                .HasForeignKey(si => si.SaleId);

            modelBuilder.Entity<SaleItem>()
                .HasOne(si => si.Product)
                .WithMany()
                .HasForeignKey(si => si.ProductId);
        }
    }
}
