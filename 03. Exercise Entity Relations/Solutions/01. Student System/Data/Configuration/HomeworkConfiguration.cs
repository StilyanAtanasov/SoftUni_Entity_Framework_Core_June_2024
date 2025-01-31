using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configuration;

public class HomeworkConfiguration : IEntityTypeConfiguration<Homework>
{
    public void Configure(EntityTypeBuilder<Homework> builder)
    {
        builder
            .HasKey(h => h.HomeworkId);

        builder
            .Property(h => h.Content)
            .HasColumnType("VARCHAR")
            .IsRequired();
    }
}