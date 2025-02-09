using BookShop.Initializer;
using Microsoft.EntityFrameworkCore;

namespace BookShop;

using Data;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        string input = Console.ReadLine()!;
        Console.WriteLine(GetBookTitlesContaining(dbContext, input));
    }

    public static string GetBookTitlesContaining(BookShopContext context, string input)
    {
        string[] books = context.Books
            .AsNoTracking()
            .Where(b => b.Title.ToLower().Contains(input.ToLower()))
            .Select(a => a.Title)
            .OrderBy(a => a)
            .ToArray();

        return string.Join(Environment.NewLine, books);
    }
}