using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static TravelAgency.Data.Constraints.EntityConstraints.Customer;

namespace TravelAgency.Data.Models.Config;

public class CustomerConfig : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .Property(c => c.PhoneNumber)
            .HasColumnType($"CHAR({PhoneNumberLength})");
    }
}