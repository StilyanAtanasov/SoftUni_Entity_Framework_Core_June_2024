using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configuration;

public class CourseConfiguration : IEntityTypeConfiguration<Course>
{
    public void Configure(EntityTypeBuilder<Course> builder)
    {
        builder
            .HasKey(c => c.CourseId);

        builder
            .Property(c => c.Name)
            .HasColumnType("NVARCHAR(80)")
            .IsRequired();

        builder
            .Property(c => c.Description)
            .HasColumnType("NVARCHAR");

        builder
            .HasMany(c => c.Resources)
            .WithOne(r => r.Course)
            .HasForeignKey(r => r.CourseId);

        builder
            .HasMany(c => c.Homeworks)
            .WithOne(r => r.Course)
            .HasForeignKey(r => r.CourseId);
    }
}