using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configuration;

public class ResourceConfiguration : IEntityTypeConfiguration<Resource>
{
    public void Configure(EntityTypeBuilder<Resource> builder)
    {
        builder
            .HasKey(r => r.ResourceId);

        builder
            .Property(r => r.Name)
            .HasColumnType("NVARCHAR(50)")
            .IsRequired();

        builder
            .Property(r => r.Url)
            .HasColumnType("VARCHAR");
    }
}