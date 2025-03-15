using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Blog.Common.EntityConstraints.ApplicationUser;

namespace Blog.Data.EntityConfiguration;

public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Username)
            .IsRequired()
            .HasMaxLength(UserNameMaxLength);

        builder.Property(e => e.Email)
            .IsRequired()
            .HasMaxLength(EmailMaxLength);

        builder.Property(e => e.Password)
            .IsRequired()
            .HasMaxLength(HashedPasswordMaxLength);

        builder
            .HasIndex(e => e.Username)
            .IsUnique();

        builder
            .HasIndex(e => e.Email)
            .IsUnique();
    }
}