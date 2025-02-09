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

        string ageRestriction = Console.ReadLine()!;
        Console.WriteLine(GetBooksByAgeRestriction(dbContext, ageRestriction));
    }

    public static string GetBooksByAgeRestriction(BookShopContext context, string command)
    {
        if (!Enum.TryParse<AgeRestriction>(command, true, out var ageRestriction))
            return string.Empty;

        string[] titles = context.Books
            .Where(b => b.AgeRestriction == ageRestriction)
            .Select(b => b.Title)
            .OrderBy(b => b)
            .ToArray();

        StringBuilder sb = new();
        foreach (string title in titles) sb.AppendLine(title);

        return sb.ToString().Trim();
    }
}