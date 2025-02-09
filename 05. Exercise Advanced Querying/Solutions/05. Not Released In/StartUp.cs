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

        int year = int.Parse(Console.ReadLine()!);
        Console.WriteLine(GetBooksNotReleasedIn(dbContext, year));
    }

    public static string GetBooksNotReleasedIn(BookShopContext context, int year)
    {
        string[] titles = context.Books
            .Where(b => b.ReleaseDate != null && b.ReleaseDate.Value.Year != year)
            .OrderBy(b => b.BookId)
            .Select(b => b.Title)
            .ToArray();

        StringBuilder sb = new();
        foreach (string t in titles) sb.AppendLine(t);

        return sb.ToString().Trim();
    }
}