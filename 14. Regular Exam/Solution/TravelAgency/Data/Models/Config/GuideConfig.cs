using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TravelAgency.Data.Models.Enums;

namespace TravelAgency.Data.Models.Config;

public class GuideConfig : IEntityTypeConfiguration<Guide>
{
    public void Configure(EntityTypeBuilder<Guide> builder)
    {
        builder.HasData(
            new Guide { Id = 1, FullName = "John Doe", Language = Language.Russian },
            new Guide { Id = 2, FullName = "Jane Smith", Language = Language.English },
            new Guide { Id = 3, FullName = "Alex Johnson", Language = Language.Spanish },
            new Guide { Id = 4, FullName = "Emily Davis", Language = Language.French },
            new Guide { Id = 5, FullName = "Michael Brown", Language = Language.German },
            new Guide { Id = 6, FullName = "Sarah Wilson", Language = Language.Russian },
            new Guide { Id = 7, FullName = "David Lee", Language = Language.English },
            new Guide { Id = 8, FullName = "Laura Garcia", Language = Language.German },
            new Guide { Id = 9, FullName = "Chris Martin", Language = Language.Spanish },
            new Guide { Id = 10, FullName = "Anna Thompson", Language = Language.French }
            );
    }
}