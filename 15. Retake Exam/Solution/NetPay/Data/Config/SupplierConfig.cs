using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using NetPay.Data.Models;

namespace NetPay.Data.Config;

public class SupplierConfig : IEntityTypeConfiguration<Supplier>
{
    public void Configure(EntityTypeBuilder<Supplier> builder)
    {
        builder
            .HasData(
            new Supplier { Id = 1, SupplierName = "E-Service" },
            new Supplier { Id = 2, SupplierName = "Light" },
            new Supplier { Id = 3, SupplierName = "Energy-PRO" },
            new Supplier { Id = 4, SupplierName = "ZEC" },
            new Supplier { Id = 5, SupplierName = "Cellular" },
            new Supplier { Id = 6, SupplierName = "A2one" },
            new Supplier { Id = 7, SupplierName = "Telecom" },
            new Supplier { Id = 8, SupplierName = "Cell2U" },
            new Supplier { Id = 9, SupplierName = "DigiTV" },
            new Supplier { Id = 10, SupplierName = "NetCom" },
            new Supplier { Id = 11, SupplierName = "Net1" },
            new Supplier { Id = 12, SupplierName = "MaxTel" },
            new Supplier { Id = 13, SupplierName = "WaterSupplyCentral" },
            new Supplier { Id = 14, SupplierName = "WaterSupplyNorth" },
            new Supplier { Id = 15, SupplierName = "WaterSupplySouth" },
            new Supplier { Id = 16, SupplierName = "FiberScreen" },
            new Supplier { Id = 17, SupplierName = "SpeedNet" },
            new Supplier { Id = 18, SupplierName = "GasGas" },
            new Supplier { Id = 19, SupplierName = "BlueHome" },
            new Supplier { Id = 20, SupplierName = "SecureHouse" },
            new Supplier { Id = 21, SupplierName = "HomeGuard" },
            new Supplier { Id = 22, SupplierName = "SafeHome" });
    }
}