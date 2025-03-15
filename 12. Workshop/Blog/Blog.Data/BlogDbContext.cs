using Blog.Core.Entities;
using Blog.Data.EntityConfiguration;
using Microsoft.EntityFrameworkCore;

namespace Blog.Data;

public class BlogDbContext : DbContext
{
	public BlogDbContext() { }

	public BlogDbContext(DbContextOptions<BlogDbContext> options) : base(options) { }

	public DbSet<ApplicationUser> ApplicationUsers { get; set; } = null!;

	public DbSet<Article> Articles { get; set; } = null!;

	protected override void OnModelCreating(ModelBuilder modelBuilder)
	{
		base.OnModelCreating(modelBuilder);
		modelBuilder.ApplyConfiguration(new ApplicationUserConfiguration());
		modelBuilder.ApplyConfiguration(new ArticleConfiguration());
	}
}