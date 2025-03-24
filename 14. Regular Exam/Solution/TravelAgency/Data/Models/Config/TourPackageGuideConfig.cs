using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace TravelAgency.Data.Models.Config;

public class TourPackageGuideConfig : IEntityTypeConfiguration<TourPackageGuide>
{
    public void Configure(EntityTypeBuilder<TourPackageGuide> builder)
    {
        builder
            .HasKey(tpg => new { tpg.GuideId, tpg.TourPackageId });

        builder.HasData(
            new TourPackageGuide { TourPackageId = 1, GuideId = 1 },
            new TourPackageGuide { TourPackageId = 1, GuideId = 2 },
            new TourPackageGuide { TourPackageId = 1, GuideId = 3 },
            new TourPackageGuide { TourPackageId = 2, GuideId = 4 },
            new TourPackageGuide { TourPackageId = 2, GuideId = 5 },
            new TourPackageGuide { TourPackageId = 2, GuideId = 6 },
            new TourPackageGuide { TourPackageId = 3, GuideId = 7 },
            new TourPackageGuide { TourPackageId = 3, GuideId = 8 },
            new TourPackageGuide { TourPackageId = 3, GuideId = 9 },
            new TourPackageGuide { TourPackageId = 4, GuideId = 10 },
            new TourPackageGuide { TourPackageId = 4, GuideId = 1 },
            new TourPackageGuide { TourPackageId = 4, GuideId = 2 },
            new TourPackageGuide { TourPackageId = 5, GuideId = 3 },
            new TourPackageGuide { TourPackageId = 5, GuideId = 4 },
            new TourPackageGuide { TourPackageId = 5, GuideId = 5 },
            new TourPackageGuide { TourPackageId = 6, GuideId = 6 },
            new TourPackageGuide { TourPackageId = 6, GuideId = 7 },
            new TourPackageGuide { TourPackageId = 6, GuideId = 8 },
            new TourPackageGuide { TourPackageId = 7, GuideId = 9 },
            new TourPackageGuide { TourPackageId = 7, GuideId = 10 },
            new TourPackageGuide { TourPackageId = 7, GuideId = 1 },
            new TourPackageGuide { TourPackageId = 8, GuideId = 2 },
            new TourPackageGuide { TourPackageId = 8, GuideId = 3 },
            new TourPackageGuide { TourPackageId = 8, GuideId = 4 },
            new TourPackageGuide { TourPackageId = 9, GuideId = 5 },
            new TourPackageGuide { TourPackageId = 9, GuideId = 6 },
            new TourPackageGuide { TourPackageId = 9, GuideId = 7 }
            );
    }
}