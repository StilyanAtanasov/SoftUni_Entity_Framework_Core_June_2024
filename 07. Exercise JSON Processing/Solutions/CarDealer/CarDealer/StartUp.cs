using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace CarDealer;

public class StartUp
{
    public static readonly JsonSerializerOptions ImportSerializerSettings = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public static readonly JsonSerializerOptions ExportSerializerSettings = new()
    {
        WriteIndented = true
    };

    public static void Main()
    {
        CarDealerContext context = new();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // --- Query 9. Import Suppliers ---
        string suppliers = File.ReadAllText("../../../Datasets/suppliers.json");
        Console.WriteLine(ImportSuppliers(context, suppliers));

        // --- Query 10. Import Parts ---
        string parts = File.ReadAllText("../../../Datasets/parts.json");
        Console.WriteLine(ImportParts(context, parts));

        // --- Query 11. Import Cars ---
        string cars = File.ReadAllText("../../../Datasets/cars.json");
        Console.WriteLine(ImportCars(context, cars));

        // --- Query 12. Import Customers ---
        string customers = File.ReadAllText("../../../Datasets/customers.json");
        Console.WriteLine(ImportCustomers(context, customers));

        // --- Query 13. Import Sales ---
        string sales = File.ReadAllText("../../../Datasets/sales.json");
        Console.WriteLine(ImportSales(context, sales));

        // --- Query 14. Export Ordered Customers ---
        Console.WriteLine(GetOrderedCustomers(context));

        // --- Query 15. Export Cars from Make Toyota ---
        Console.WriteLine(GetCarsFromMakeToyota(context));

        // --- Query 16. Export Local Suppliers ---
        Console.WriteLine(GetLocalSuppliers(context));

        // --- Query 17. Export Cars with Their List of Parts ---
        Console.WriteLine(GetCarsWithTheirListOfParts(context));

        // --- Query 18. Export Total Sales By Customer ---
        Console.WriteLine(GetTotalSalesByCustomer(context));

        // --- Query 19. Export Sales with Applied Discount ---
        Console.WriteLine(GetSalesWithAppliedDiscount(context));
    }

    // --- Query 9. Import Suppliers ---
    public static string ImportSuppliers(CarDealerContext context, string inputJson)
    {
        ImportSupplierDto[] suppliersDto = JsonSerializer.Deserialize<ImportSupplierDto[]>(inputJson, ImportSerializerSettings)!;

        Supplier[] suppliers = suppliersDto
            .Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter
            })
            .ToArray();

        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();

        return $"Successfully imported {suppliers.Length}.";
    }

    // --- Query 10. Import Parts ---
    public static string ImportParts(CarDealerContext context, string inputJson)
    {
        ImportPartDto[] partsDto = JsonSerializer.Deserialize<ImportPartDto[]>(inputJson, ImportSerializerSettings)!;

        Part[] parts = partsDto
            .Where(p => context.Suppliers.Any(s => s.Id == p.SupplierId))
            .Select(p => new Part
            {
                Name = p.Name,
                Price = p.Price,
                Quantity = p.Quantity,
                SupplierId = p.SupplierId
            })
            .ToArray();

        context.Parts.AddRange(parts);
        context.SaveChanges();

        return $"Successfully imported {parts.Length}.";
    }

    // --- Query 11. Import Cars ---
    public static string ImportCars(CarDealerContext context, string inputJson)
    {
        ImportCarDto[] carsDto = JsonSerializer.Deserialize<ImportCarDto[]>(inputJson, ImportSerializerSettings)!;

        Car[] cars = carsDto
            .Select(c => new Car
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance,
                PartsCars = c.PartsId
                    .Where(p => context.Parts.Any(pt => pt.Id == p))
                    .Distinct()
                    .Select(p => new PartCar { PartId = p })
                    .ToArray()
            })
            .ToArray();

        context.Cars.AddRange(cars);
        context.SaveChanges();

        return $"Successfully imported {cars.Length}.";
    }

    // --- Query 12. Import Customers ---
    public static string ImportCustomers(CarDealerContext context, string inputJson)
    {
        ImportCustomerDto[] customersDto = JsonSerializer.Deserialize<ImportCustomerDto[]>(inputJson, ImportSerializerSettings)!;

        Customer[] customers = customersDto
            .Select(c => new Customer
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
            .ToArray();

        context.Customers.AddRange(customers);
        context.SaveChanges();

        return $"Successfully imported {customers.Length}.";
    }

    // --- Query 13 Import Sales ---
    public static string ImportSales(CarDealerContext context, string inputJson)
    {
        ImportSaleDto[] salesDto = JsonSerializer.Deserialize<ImportSaleDto[]>(inputJson, ImportSerializerSettings)!;

        Sale[] sales = salesDto
            .Select(s => new Sale
            {
                Discount = s.Discount,
                CustomerId = s.CustomerId,
                CarId = s.CarId
            })
            .ToArray();

        context.Sales.AddRange(sales);
        context.SaveChanges();

        return $"Successfully imported {sales.Length}.";
    }

    // --- Query 14. Export Ordered Customers ---
    public static string GetOrderedCustomers(CarDealerContext context)
    {
        ExportCustomerDto[] customers = context.Customers
            .AsNoTracking()
            .Select(c => new ExportCustomerDto
            {
                Name = c.Name,
                BirthDate = c.BirthDate,
                IsYoungDriver = c.IsYoungDriver
            })
            .OrderBy(c => c.BirthDate)
            .ThenBy(c => c.IsYoungDriver)
            .ToArray();

        string json = JsonSerializer.Serialize(customers, ExportSerializerSettings);
        // File.WriteAllText("../../../Results/ordered-customers.json", json);

        return json;
    }

    // --- Query 15. Export Cars from Make Toyota ---
    public static string GetCarsFromMakeToyota(CarDealerContext context)
    {
        ExportCarDto[] customers = context.Cars
            .AsNoTracking()
            .Where(c => c.Make == "Toyota")
            .Select(c => new ExportCarDto
            {
                Id = c.Id,
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance
            })
            .OrderBy(c => c.Model)
            .ThenByDescending(c => c.TraveledDistance)
            .ToArray();

        string json = JsonSerializer.Serialize(customers, ExportSerializerSettings);
        File.WriteAllText("../../../Results/toyota-cars.json", json);

        return json;
    }

    // --- Query 16. Export Local Suppliers ---
    public static string GetLocalSuppliers(CarDealerContext context)
    {
        ExportSupplierDto[] customers = context.Suppliers
            .AsNoTracking()
            .Where(s => !s.IsImporter)
            .Select(s => new ExportSupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                PartsCount = s.Parts.Count
            })
            .ToArray();

        string json = JsonSerializer.Serialize(customers, ExportSerializerSettings);
        // File.WriteAllText("../../../Results/local-suppliers.json", json);

        return json;
    }

    // --- Query 17. Export Cars with Their List of Parts ---
    public static string GetCarsWithTheirListOfParts(CarDealerContext context)
    {

        ExportCarPartsDto[] customers = context.Cars
            .AsNoTracking()
            .Select(cp => new ExportCarPartsDto
            {
                Car = new ExportCarWithoutIdDto
                {
                    Make = cp.Make,
                    Model = cp.Model,
                    TraveledDistance = cp.TraveledDistance
                },
                Parts = cp.PartsCars
                    .Select(pc => new ExportPartDto
                    {
                        Name = pc.Part.Name,
                        Price = pc.Part.Price.ToString("F2"),
                    })
                    .ToHashSet()
            })
            .ToArray();

        string json = JsonSerializer.Serialize(customers, ExportSerializerSettings);
        // File.WriteAllText("../../../Results/cars-and-parts.json", json);

        return json;
    }

    // --- Query 18. Export Total Sales By Customer ---
    public static string GetTotalSalesByCustomer(CarDealerContext context)
    {
        ExportCustomerBoughtCarsDto[] customers = context.Customers
            .AsNoTracking()
            .Where(c => c.Sales.Any())
            .Select(c => new ExportCustomerBoughtCarsDto
            {
                FullName = c.Name,
                BoughtCars = c.Sales.Count,
                SpentMoney = c.Sales
                    .SelectMany(s => s.Car.PartsCars)
                    .Sum(pc => pc.Part.Price)
            })
            .OrderByDescending(c => c.SpentMoney)
            .ThenByDescending(c => c.BoughtCars)
            .ToArray();

        string json = JsonSerializer.Serialize(customers, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        // File.WriteAllText("../../../Results/customers-total-sales.json", json);

        return json;
    }

    // --- Query 19. Export Sales with Applied Discount ---
    public static string GetSalesWithAppliedDiscount(CarDealerContext context)
    {
        ExportSalesDto[] sales = context.Sales
            .AsNoTracking()
            .Select(s => new ExportSalesDto
            {
                Car = new ExportCarWithoutIdDto
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TraveledDistance = s.Car.TraveledDistance
                },
                CustomerName = s.Customer.Name,
                Discount = s.Discount.ToString("F2"),
                Price = s.Car.PartsCars.Sum(pc => pc.Part.Price).ToString("F2"),
                PriceWithDiscount = (s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - s.Discount / 100)).ToString("F2")
            })
            .Take(10)
            .ToArray();

        string json = JsonSerializer.Serialize(sales, new JsonSerializerOptions
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });
        // File.WriteAllText("../../../Results/sales-discounts.json", json);

        return json;
    }
}