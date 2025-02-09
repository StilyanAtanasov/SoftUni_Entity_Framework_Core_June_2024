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

        Console.WriteLine(GetMostRecentBooks(dbContext));
    }

    public static string GetMostRecentBooks(BookShopContext context)
    {
        var mostRecentBooks = context.Categories
            .AsNoTracking()
            .Select(c => new
            {
                CategoryName = c.Name,
                RecentBooks = c.CategoryBooks
                    .OrderByDescending(b => b.Book.ReleaseDate)
                    .Select(b => new
                    {
                        BookName = b.Book.Title,
                        b.Book.ReleaseDate
                    })
                    .Take(3)
                    .ToArray()
            })
            .OrderBy(c => c.CategoryName)
            .ToArray();

        StringBuilder sb = new();
        foreach (var mrb in mostRecentBooks)
        {
            sb.AppendLine($"--{mrb.CategoryName}");
            foreach (var b in mrb.RecentBooks) sb.AppendLine($"{b.BookName} ({b.ReleaseDate!.Value.Year})");
        }

        return sb.ToString().Trim();
    }
}