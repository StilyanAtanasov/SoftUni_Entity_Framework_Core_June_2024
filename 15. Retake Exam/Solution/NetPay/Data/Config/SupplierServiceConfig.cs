using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPay.Data.Models;

namespace NetPay.Data.Config;

public class SupplierServiceConfig : IEntityTypeConfiguration<SupplierService>
{
    public void Configure(EntityTypeBuilder<SupplierService> builder)
    {
        builder
            .HasKey(ss => new { ss.ServiceId, ss.SupplierId });

        builder
            .HasData(
             new SupplierService { SupplierId = 1, ServiceId = 1 },
             new SupplierService { SupplierId = 2, ServiceId = 1 },
             new SupplierService { SupplierId = 3, ServiceId = 1 },
             new SupplierService { SupplierId = 4, ServiceId = 1 },
             new SupplierService { SupplierId = 5, ServiceId = 3 },
             new SupplierService { SupplierId = 5, ServiceId = 4 },
             new SupplierService { SupplierId = 5, ServiceId = 5 },
             new SupplierService { SupplierId = 6, ServiceId = 3 },
             new SupplierService { SupplierId = 6, ServiceId = 4 },
             new SupplierService { SupplierId = 6, ServiceId = 5 },
             new SupplierService { SupplierId = 6, ServiceId = 6 },
             new SupplierService { SupplierId = 7, ServiceId = 3 },
             new SupplierService { SupplierId = 7, ServiceId = 4 },
             new SupplierService { SupplierId = 7, ServiceId = 5 },
             new SupplierService { SupplierId = 8, ServiceId = 3 },
             new SupplierService { SupplierId = 8, ServiceId = 4 },
             new SupplierService { SupplierId = 8, ServiceId = 5 },
             new SupplierService { SupplierId = 9, ServiceId = 3 },
             new SupplierService { SupplierId = 9, ServiceId = 4 },
             new SupplierService { SupplierId = 10, ServiceId = 3 },
             new SupplierService { SupplierId = 10, ServiceId = 4 },
             new SupplierService { SupplierId = 10, ServiceId = 6 },
             new SupplierService { SupplierId = 11, ServiceId = 3 },
             new SupplierService { SupplierId = 11, ServiceId = 4 },
             new SupplierService { SupplierId = 12, ServiceId = 3 },
             new SupplierService { SupplierId = 12, ServiceId = 4 },
             new SupplierService { SupplierId = 12, ServiceId = 5 },
             new SupplierService { SupplierId = 12, ServiceId = 6 },
             new SupplierService { SupplierId = 13, ServiceId = 2 },
             new SupplierService { SupplierId = 14, ServiceId = 2 },
             new SupplierService { SupplierId = 15, ServiceId = 2 },
             new SupplierService { SupplierId = 16, ServiceId = 4 },
             new SupplierService { SupplierId = 16, ServiceId = 3 },
             new SupplierService { SupplierId = 17, ServiceId = 3 },
             new SupplierService { SupplierId = 17, ServiceId = 4 },
             new SupplierService { SupplierId = 17, ServiceId = 6 },
             new SupplierService { SupplierId = 18, ServiceId = 7 },
             new SupplierService { SupplierId = 19, ServiceId = 7 },
             new SupplierService { SupplierId = 20, ServiceId = 6 },
             new SupplierService { SupplierId = 21, ServiceId = 6 },
             new SupplierService { SupplierId = 22, ServiceId = 6 });
    }
}