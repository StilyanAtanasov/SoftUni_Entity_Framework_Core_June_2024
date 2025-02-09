using BookShop.Initializer;
using System.Text;

namespace BookShop;

using Data;
using Models.Enums;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        Console.WriteLine(GetGoldenBooks(dbContext));
    }

    public static string GetGoldenBooks(BookShopContext context)
    {
        string[] titles = context.Books
            .Where(b => b.EditionType == EditionType.Gold && b.Copies < 5_000)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .ToArray();

        StringBuilder sb = new();
        foreach (string title in titles) sb.AppendLine(title);

        return sb.ToString().Trim();
    }
}