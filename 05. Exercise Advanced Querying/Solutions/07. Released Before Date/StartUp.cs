using BookShop.Initializer;
using Microsoft.EntityFrameworkCore;

namespace BookShop;

using Data;
using System.Globalization;
using System.Text;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        string date = Console.ReadLine()!;
        Console.WriteLine(GetBooksReleasedBefore(dbContext, date));
    }

    public static string GetBooksReleasedBefore(BookShopContext context, string date)
    {
        var books = context.Books
            .AsNoTracking()
            .Where(b => b.ReleaseDate < DateTime.ParseExact(date, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None))
            .OrderByDescending(b => b.ReleaseDate)
            .Select(b => new
            {
                b.Title,
                b.EditionType,
                b.Price
            })
            .ToArray();

        StringBuilder sb = new();
        foreach (var b in books) sb.AppendLine($"{b.Title} - {b.EditionType} - ${b.Price:F2}");

        return sb.ToString().Trim();
    }
}