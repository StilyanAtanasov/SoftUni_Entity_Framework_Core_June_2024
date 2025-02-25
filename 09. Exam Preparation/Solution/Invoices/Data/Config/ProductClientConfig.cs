using Invoices.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Invoices.Data.Config;

public class ProductClientConfig : IEntityTypeConfiguration<ProductClient>
{
    public void Configure(EntityTypeBuilder<ProductClient> builder)
    {
        builder.HasKey(x => new { x.ClientId, x.ProductId });
    }
}