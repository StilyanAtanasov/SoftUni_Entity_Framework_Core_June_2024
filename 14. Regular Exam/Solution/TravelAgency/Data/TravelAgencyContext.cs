using Microsoft.EntityFrameworkCore;
using TravelAgency.Data.Models;
using TravelAgency.Data.Models.Config;

namespace TravelAgency.Data
{
    public class TravelAgencyContext : DbContext
    {
        private const string ConnectionString = @"Server=.;Database=TravelAgency;Trusted_Connection=True;TrustServerCertificate=True";

        public TravelAgencyContext() { }

        public TravelAgencyContext(DbContextOptions options) : base(options) { }

        public DbSet<Customer> Customers { get; set; } = null!;

        public DbSet<Guide> Guides { get; set; } = null!;

        public DbSet<Booking> Bookings { get; set; } = null!;

        public DbSet<TourPackage> TourPackages { get; set; } = null!;

        public DbSet<TourPackageGuide> TourPackagesGuides { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured) optionsBuilder.UseSqlServer(ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerConfig());
            modelBuilder.ApplyConfiguration(new GuideConfig());
            modelBuilder.ApplyConfiguration(new TourPackageConfig());
            modelBuilder.ApplyConfiguration(new TourPackageGuideConfig());
        }
    }
}
