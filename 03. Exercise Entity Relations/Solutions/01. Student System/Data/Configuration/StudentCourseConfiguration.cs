using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_StudentSystem.Data.Models;

namespace P01_StudentSystem.Data.Configuration;

public class StudentCourseConfiguration : IEntityTypeConfiguration<StudentCourse>
{
    public void Configure(EntityTypeBuilder<StudentCourse> builder)
    {
        builder
            .HasKey(sc => new { sc.StudentId, sc.CourseId });

        builder
            .HasOne(sc => sc.Student)
            .WithMany(s => s.StudentsCourses)
            .HasForeignKey(sc => sc.StudentId);

        builder
            .HasOne(sc => sc.Course)
            .WithMany(s => s.StudentsCourses)
            .HasForeignKey(sc => sc.CourseId);
    }
}