using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPay.Data.Models;
using static NetPay.Data.Constraints.EntityConstraints.Household;

namespace NetPay.Data.Config;

public class HouseholdConfig : IEntityTypeConfiguration<Household>
{
    public void Configure(EntityTypeBuilder<Household> builder)
    {
        builder
            .Property(h => h.PhoneNumber)
            .HasColumnType($"CHAR({PhoneNumberLength})");
    }
}