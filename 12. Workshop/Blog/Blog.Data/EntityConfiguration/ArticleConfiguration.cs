using Blog.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using static Blog.Common.EntityConstraints.Article;

namespace Blog.Data.EntityConfiguration;

public class ArticleConfiguration : IEntityTypeConfiguration<Article>
{
	public void Configure(EntityTypeBuilder<Article> builder)
	{
		builder.HasKey(x => x.Id);

		builder.Property(e => e.Title)
			.IsRequired()
			.HasMaxLength(TitleMaxLength);

		builder.Property(e => e.Content)
			.IsRequired()
			.HasMaxLength(ContentMaxLength);

		builder.Property(e => e.CreatedOn)
			.IsRequired();

		builder.HasOne(e => e.Author)
			.WithMany()
			.HasForeignKey(e => e.AuthorId)
			.OnDelete(DeleteBehavior.Restrict);
	}
}