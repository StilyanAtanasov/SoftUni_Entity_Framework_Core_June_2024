using BookShop.Initializer;
using System.Text;

namespace BookShop;

using Data;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        Console.WriteLine(GetBooksByPrice(dbContext));
    }

    public static string GetBooksByPrice(BookShopContext context)
    {
        var books = context.Books
            .Where(b => b.Price > 40)
            .OrderByDescending(b => b.Price)
            .Select(b => new
            {
                b.Title,
                b.Price
            })
            .ToArray();

        StringBuilder sb = new();
        foreach (var b in books) sb.AppendLine($"{b.Title} - ${b.Price:F2}");

        return sb.ToString().Trim();
    }
}