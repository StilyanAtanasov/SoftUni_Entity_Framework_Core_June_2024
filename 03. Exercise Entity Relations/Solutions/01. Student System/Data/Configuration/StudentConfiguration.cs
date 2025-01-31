using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configuration;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder
            .HasKey(s => s.StudentId);

        builder
            .Property(s => s.Name)
            .HasColumnType("NVARCHAR(100)")
            .IsRequired();

        builder
            .Property(s => s.PhoneNumber)
            .HasColumnType("CHAR(10)");

        builder
            .Property(s => s.RegisteredOn)
            .ValueGeneratedOnAdd();

        builder
            .HasMany(s => s.Homeworks)
            .WithOne(h => h.Student)
            .HasForeignKey(h => h.StudentId);
    }
}