using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelAgency.Data.Models.Config;

public class TourPackageConfig : IEntityTypeConfiguration<TourPackage>
{
    public void Configure(EntityTypeBuilder<TourPackage> builder)
    {
        builder
            .HasData(
                new TourPackage
                {
                    Id = 1,
                    PackageName = "Horse Riding Tour",
                    Description = "Experience the thrill of a guided horse riding tour through picturesque landscapes. Suitable for all skill levels. Enjoy nature and create unforgettable memories. Duration: 3 hours.",
                    Price = 199.99m
                },
                new TourPackage
                {
                    Id = 2,
                    PackageName = "Sightseeing Tour",
                    Description = "Explore the city's top attractions with a guided sightseeing tour. Visit historical landmarks, iconic buildings, and scenic spots. Perfect for all ages. Duration: 4 hours.",
                    Price = 149.99m
                },
                new TourPackage
                {
                    Id = 3,
                    PackageName = "Diving Tour",
                    Description = "Dive into the crystal-clear waters and explore vibrant coral reefs and marine life. Suitable for beginners and experienced divers. Equipment provided. Duration: 2 hours.",
                    Price = 299.99m
                },
                new TourPackage
                {
                    Id = 4,
                    PackageName = "Mountain Hiking",
                    Description = "Embark on an exhilarating mountain hiking tour. Enjoy breathtaking views, fresh air, and challenging trails. Suitable for all fitness levels. Duration: 5 hours.",
                    Price = 179.99m
                },
                new TourPackage
                {
                    Id = 5,
                    PackageName = "City Tour",
                    Description = "Discover the charm of the city with a guided tour. Visit famous landmarks, bustling markets, and hidden gems. Perfect for all ages. Duration: 3 hours.",
                    Price = 129.99m
                },
                new TourPackage
                {
                    Id = 6,
                    PackageName = "Food Tour",
                    Description = "Savor the local flavors on a guided food tour. Taste a variety of dishes, visit top eateries, and learn about the culinary culture. Suitable for all food lovers. Duration: 3 hours.",
                    Price = 99.99m
                },
                new TourPackage
                {
                    Id = 7,
                    PackageName = "Wildlife Safari",
                    Description = "Embark on an exciting wildlife safari. Spot exotic animals in their natural habitat, guided by experts. Perfect for nature enthusiasts. Duration: 4 hours.",
                    Price = 249.99m
                },
                new TourPackage
                {
                    Id = 8,
                    PackageName = "Historical Sites",
                    Description = "Explore ancient ruins, museums, and landmarks on a guided tour. Learn about the rich history and culture of the area. Ideal for history buffs. Duration: 4 hours.",
                    Price = 159.99m
                },
                new TourPackage
                {
                    Id = 9,
                    PackageName = "Sunset Cruise",
                    Description = "Experience a breathtaking sunset on a luxury cruise. Enjoy stunning views, delicious refreshments, and a relaxing atmosphere. Perfect for couples and families. Duration: 2 hours.",
                    Price = 399.99m
                }
            );
    }
}