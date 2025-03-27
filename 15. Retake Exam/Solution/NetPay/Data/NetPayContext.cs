using Microsoft.EntityFrameworkCore;
using NetPay.Data.Config;
using NetPay.Data.Models;

namespace NetPay.Data
{
    public class NetPayContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=NetPay;Trusted_Connection=True;TrustServerCertificate=True";

        public NetPayContext() { }

        public NetPayContext(DbContextOptions options) : base(options) { }

        public DbSet<Household> Households { get; set; } = null!;

        public DbSet<Expense> Expenses { get; set; } = null!;

        public DbSet<Service> Services { get; set; } = null!;

        public DbSet<Supplier> Suppliers { get; set; } = null!;

        public DbSet<SupplierService> SuppliersServices { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new SupplierServiceConfig());
            modelBuilder.ApplyConfiguration(new SupplierConfig());
            modelBuilder.ApplyConfiguration(new ServiceConfig());
        }
    }
}
