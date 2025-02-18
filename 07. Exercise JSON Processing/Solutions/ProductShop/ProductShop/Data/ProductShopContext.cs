using Microsoft.EntityFrameworkCore;
using ProductShop.Models;
namespace ProductShop.Data;

public class ProductShopContext : DbContext
{
    public ProductShopContext()
    {
    }

    public ProductShopContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<Category> Categories { get; set; } = null!;

    public DbSet<Product> Products { get; set; } = null!;

    public DbSet<User> Users { get; set; } = null!;

    public DbSet<CategoryProduct> CategoriesProducts { get; set; } = null!;


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder
                .UseSqlServer(Configuration.ConnectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CategoryProduct>(entity =>
        {
            entity.HasKey(x => new { x.CategoryId, x.ProductId });
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity
                .HasOne(x => x.Seller)
                .WithMany(x => x.ProductsSold)
                .HasForeignKey(x => x.SellerId)
                .OnDelete(DeleteBehavior.NoAction);

            entity
                .HasOne(x => x.Buyer)
                .WithMany(x => x.ProductsBought)
                .HasForeignKey(x => x.BuyerId)
                .OnDelete(DeleteBehavior.NoAction);
        });
    }
}