using P03_SalesDatabase.Data;
using P03_SalesDatabase.Data.Models;

namespace P03_SalesDatabase;

public class Startup
{
    private static readonly Random Random = new();

    private static readonly string[] ProductNames = {
        "Laptop", "Smartphone", "Tablet", "Monitor", "Keyboard",
        "Mouse", "Printer", "Headphones", "Speaker", "Camera",
        "Smartwatch", "Router", "External Hard Drive", "USB Flash Drive",
        "Charger", "Battery", "Earphones", "Microphone", "Webcam", "Projector"
    };

    private static readonly string[] CustomerFirstNames = {
        "John", "Jane", "Michael", "Emily", "David",
        "Sarah", "James", "Linda", "Robert", "Karen",
        "William", "Susan", "Joseph", "Maria", "Thomas",
        "Nancy", "Charles", "Lisa", "Daniel", "Betty"
    };

    private static readonly string[] CustomerLastNames = {
        "Smith", "Johnson", "Williams", "Brown", "Jones",
        "Garcia", "Miller", "Davis", "Rodriguez", "Martinez",
        "Hernandez", "Lopez", "Gonzalez", "Wilson", "Anderson",
        "Thomas", "Taylor", "Moore", "Jackson", "Martin"
    };

    private static readonly string[] StoreNames = {
        "Tech Haven", "Gadget World", "Electro Hub", "Future Tech",
        "Digital Planet", "Smart Solutions", "Innovate Store",
        "Tech Universe", "Gizmo Central", "Electro World"
    };

    private static readonly string[] EmailDomains = new[]
    {
        "gmail.com", "yahoo.com", "outlook.com", "hotmail.com", "icloud.com",
        "protonmail.com", "aol.com", "mail.com", "zoho.com", "yandex.com"
    };

    private readonly SalesContext _context;

    public Startup() => _context = new SalesContext();

    public static void Main(string[] args) => new Startup().Seed();

    public void Seed()
    {
        SalesContext context = _context;

        if (context.Products.Any() || context.Customers.Any() || context.Sales.Any() || context.Stores.Any()) return;

        // Shuffle Arrays
        Shuffle(ProductNames);
        Shuffle(StoreNames);

        // Seed Products
        List<Product> products = new();
        foreach (string name in ProductNames)
        {
            products.Add(new Product
            {
                Name = name,
                Quantity = Random.Next(1, 1000),
                Price = Random.Next(2, 4000) + (decimal)Random.NextDouble()
            });
        }

        context.Products.AddRange(products);

        // Seed Customers
        List<Customer> customers = new();
        for (int i = 0; i < 50; i++)
        {
            string firstName = CustomerFirstNames[Random.Next(CustomerFirstNames.Length)];
            string lastName = CustomerLastNames[Random.Next(CustomerLastNames.Length)];
            customers.Add(new Customer
            {
                Name = $"{firstName} {lastName}",
                Email = $"{firstName.ToLower()}.{lastName.ToLower()}@{EmailDomains[Random.Next(EmailDomains.Length)]}",
                CreditCardNumber = GenerateRandomCreditCardNumber()
            });
        }

        context.Customers.AddRange(customers);

        // Seed Stores
        List<Store> stores = new();
        foreach (var name in StoreNames) stores.Add(new Store { Name = name });

        context.Stores.AddRange(stores);

        // Seed Sales
        var sales = new List<Sale>();
        for (int i = 1; i <= 200; i++)
        {
            sales.Add(new Sale
            {
                Date = DateTime.Now.AddDays(-Random.Next(1, 365)),
                Product = products[Random.Next(products.Count)],
                Customer = customers[Random.Next(customers.Count)],
                Store = stores[Random.Next(stores.Count)]
            });
        }

        context.Sales.AddRange(sales);
        context.SaveChanges();
    }

    private static string GenerateRandomCreditCardNumber()
    {
        var cardNumber = new char[16];
        for (int i = 0; i < 16; i++) cardNumber[i] = Random.Next(0, 10).ToString()[0];

        return new string(cardNumber);
    }

    private static void Shuffle<T>(T[] array)
    {
        int n = array.Length;
        while (n > 1)
        {
            n--;
            int k = Random.Next(n + 1);
            (array[k], array[n]) = (array[n], array[k]);
        }
    }
}
