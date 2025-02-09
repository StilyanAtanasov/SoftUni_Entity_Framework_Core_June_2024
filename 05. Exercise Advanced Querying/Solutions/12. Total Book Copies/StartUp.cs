using BookShop.Initializer;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BookShop;

using Data;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        Console.WriteLine(CountCopiesByAuthor(dbContext));
    }

    public static string CountCopiesByAuthor(BookShopContext context)
    {
        var copiesByAuthor = context.Authors
            .AsNoTracking()
            .Select(a => new
            {
                AuthorName = $"{a.FirstName} {a.LastName}",
                TotalCopies = a.Books.Sum(b => b.Copies)
            })
            .OrderByDescending(a => a.TotalCopies)
            .ToArray();

        StringBuilder sb = new();
        foreach (var cba in copiesByAuthor) sb.AppendLine($"{cba.AuthorName} - {cba.TotalCopies}");

        return sb.ToString().Trim();
    }
}