using CarDealer.Data;
using CarDealer.DTOs.Export;
using CarDealer.DTOs.Import;
using CarDealer.Models;
using Microsoft.EntityFrameworkCore;
using System.Xml.Serialization;

namespace CarDealer;

public class StartUp
{
    public static void Main()
    {
        using CarDealerContext context = new();

        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();

        // --- Query 9. Import Suppliers ---
        string suppliers = File.ReadAllText("../../../Datasets/suppliers.xml");
        Console.WriteLine(ImportSuppliers(context, suppliers));

        // --- Query 10. Import Parts ---
        string parts = File.ReadAllText("../../../Datasets/parts.xml");
        Console.WriteLine(ImportParts(context, parts));

        // --- Query 11. Import Cars ---
        string cars = File.ReadAllText("../../../Datasets/cars.xml");
        Console.WriteLine(ImportCars(context, cars));

        // --- Query 12. Import Customers ---
        string customers = File.ReadAllText("../../../Datasets/customers.xml");
        Console.WriteLine(ImportCustomers(context, customers));

        // --- Query 13. Import Sales ---
        string sales = File.ReadAllText("../../../Datasets/sales.xml");
        Console.WriteLine(ImportSales(context, sales));

        // --- Query 14. Export Cars With Distance ---
        Console.WriteLine(GetCarsWithDistance(context));

        // --- Query 15. Export Cars from Make BMW ---
        Console.WriteLine(GetCarsFromMakeBmw(context));

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
    public static string ImportSuppliers(CarDealerContext context, string inputXml)
    {
        ImportSupplierDto[] suppliersDto = DeserializeXml<ImportSupplierDto[]>(inputXml, "Suppliers");

        Supplier[] suppliers = suppliersDto
            .Select(s => new Supplier
            {
                Name = s.Name,
                IsImporter = s.IsImporter
            })
            .ToArray();

        context.Suppliers.AddRange(suppliers);
        context.SaveChanges();

        return $"Successfully imported {suppliers.Length}";
    }

    // --- Query 10. Import Parts ---
    public static string ImportParts(CarDealerContext context, string inputXml)
    {
        ImportPartDto[] partsDto = DeserializeXml<ImportPartDto[]>(inputXml, "Parts");

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

        return $"Successfully imported {parts.Length}";
    }

    // --- Query 11. Import Cars ---
    public static string ImportCars(CarDealerContext context, string inputXml)
    {
        ImportCarDto[] carsDto = DeserializeXml<ImportCarDto[]>(inputXml, "Cars");

        Car[] cars = carsDto
            .Select(c => new Car
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance,
                PartsCars = c.PartsId
                    .Where(p => context.Parts.Any(pt => pt.Id == p.Id))
                    .DistinctBy(p => p.Id)
                    .Select(p => new PartCar { PartId = p.Id })
                    .ToList()
            })
            .ToArray();

        context.Cars.AddRange(cars);
        context.SaveChanges();

        return $"Successfully imported {cars.Length}";
    }

    // --- Query 12. Import Customers ---
    public static string ImportCustomers(CarDealerContext context, string inputXml)
    {
        ImportCustomerDto[] customersDto = DeserializeXml<ImportCustomerDto[]>(inputXml, "Customers");

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

        return $"Successfully imported {customers.Length}";
    }

    // --- Query 13 Import Sales ---
    public static string ImportSales(CarDealerContext context, string inputXml)
    {
        ImportSaleDto[] salesDto = DeserializeXml<ImportSaleDto[]>(inputXml, "Sales");

        Sale[] sales = salesDto
            .Where(s => context.Cars.Any(c => c.Id == s.CarId))
            .Select(s => new Sale
            {
                Discount = s.Discount,
                CustomerId = s.CustomerId,
                CarId = s.CarId
            })
            .ToArray();

        context.Sales.AddRange(sales);
        context.SaveChanges();

        return $"Successfully imported {sales.Length}";
    }

    // --- Query 14. Export Cars With Distance ---
    public static string GetCarsWithDistance(CarDealerContext context)
    {
        ExportCarDto[] cars = context.Cars
            .AsNoTracking()
            .Where(c => c.TraveledDistance > 2_000_000)
            .Select(c => new ExportCarDto
            {
                Make = c.Make,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance
            })
            .OrderBy(c => c.Make)
            .ThenBy(c => c.Model)
            .Take(10)
            .ToArray();

        string xml = SerializeXml(cars, "cars");
        // File.WriteAllText("../../../Results/cars.xml", xml);

        return xml;
    }

    // --- Query 15. Export Cars from Make BMW ---
    public static string GetCarsFromMakeBmw(CarDealerContext context)
    {
        ExportCarWithoutMakeDto[] cars = context.Cars
            .AsNoTracking()
            .Where(c => c.Make == "BMW")
            .Select(c => new ExportCarWithoutMakeDto
            {
                Id = c.Id,
                Model = c.Model,
                TraveledDistance = c.TraveledDistance
            })
            .OrderBy(c => c.Model)
            .ThenByDescending(c => c.TraveledDistance)
            .ToArray();

        string xml = SerializeXml(cars, "cars");
        // File.WriteAllText("../../../Results/bmw-cars.xml", xml);

        return xml;
    }

    // --- Query 16. Export Local Suppliers ---
    public static string GetLocalSuppliers(CarDealerContext context)
    {
        ExportSupplierDto[] suppliers = context.Suppliers
            .AsNoTracking()
            .Where(s => !s.IsImporter)
            .Select(s => new ExportSupplierDto
            {
                Id = s.Id,
                Name = s.Name,
                PartsCount = s.Parts.Count
            })
            .ToArray();

        string xml = SerializeXml(suppliers, "suppliers");
        // File.WriteAllText("../../../Results/local-suppliers.xml", xml);

        return xml;
    }

    // --- Query 17. Export Cars with Their List of Parts ---
    public static string GetCarsWithTheirListOfParts(CarDealerContext context)
    {

        ExportCarPartsDto[] carParts = context.Cars
            .AsNoTracking()
            .Select(cp => new ExportCarPartsDto
            {
                Make = cp.Make,
                Model = cp.Model,
                TraveledDistance = cp.TraveledDistance,
                Parts = cp.PartsCars
                    .Select(p => new ExportPartDto
                    {
                        Name = p.Part.Name,
                        Price = p.Part.Price,
                    })
                    .OrderByDescending(p => p.Price)
                    .ToList()
            })
            .OrderByDescending(cp => cp.TraveledDistance)
            .ThenBy(cp => cp.Model)
            .Take(5)
            .ToArray();

        string xml = SerializeXml(carParts, "cars");
        // File.WriteAllText("../../../Results/cars-and-parts.xml", xml);

        return xml;
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
                SpentMoney = c.IsYoungDriver ?
                    (decimal)c.Sales
                        .SelectMany(s => s.Car.PartsCars)
                        .Sum(pc => Math.Round((double)pc.Part.Price * 0.95, 2))
                    : (decimal)c.Sales
                        .SelectMany(s => s.Car.PartsCars)
                        .Sum(pc => (double)pc.Part.Price)
            })
            .OrderByDescending(c => c.SpentMoney)
            .ToArray();

        string xml = SerializeXml(customers, "customers");
        // File.WriteAllText("../../../Results/customers-total-sales.xml", xml);

        return xml;
    }

    // --- Query 19. Export Sales with Applied Discount ---
    public static string GetSalesWithAppliedDiscount(CarDealerContext context)
    {
        ExportSalesDto[] sales = context.Sales
            .AsNoTracking()
            .Select(s => new ExportSalesDto
            {
                Car = new ExportCarUsingAttributesDto
                {
                    Make = s.Car.Make,
                    Model = s.Car.Model,
                    TraveledDistance = s.Car.TraveledDistance
                },
                CustomerName = s.Customer.Name,
                Discount = (int)s.Discount,
                Price = s.Car.PartsCars.Sum(pc => pc.Part.Price),
                PriceWithDiscount = (double)(s.Car.PartsCars.Sum(pc => pc.Part.Price) * (1 - s.Discount / 100))
            })
            .ToArray();

        string xml = SerializeXml(sales, "sales");
        // File.WriteAllText("../../../Results/sales-discounts.xml", xml);

        return xml;
    }

    private static T DeserializeXml<T>(string xmlSerialized, string root)
    {
        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(root));
        using StringReader reader = new(xmlSerialized);

        return (T)serializer.Deserialize(reader)!;
    }

    private static string SerializeXml<T>(T dto, string rootElement)
    {
        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElement));
        using StringWriter writer = new();

        XmlSerializerNamespaces namespaces = new();
        namespaces.Add("", "");

        serializer.Serialize(writer, dto, namespaces);
        return writer.ToString();
    }
}