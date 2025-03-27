using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPay.Data.Models;

namespace NetPay.Data.Config;

public class ServiceConfig : IEntityTypeConfiguration<Service>
{
    public void Configure(EntityTypeBuilder<Service> builder)
    {
        builder
            .HasData(
             new Service { Id = 1, ServiceName = "Electricity" },
             new Service { Id = 2, ServiceName = "Water" },
             new Service { Id = 3, ServiceName = "Internet" },
             new Service { Id = 4, ServiceName = "TV" },
             new Service { Id = 5, ServiceName = "Phone" },
             new Service { Id = 6, ServiceName = "Security" },
             new Service { Id = 7, ServiceName = "Gas" });
    }
}