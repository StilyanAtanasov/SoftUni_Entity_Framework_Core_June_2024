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
        Console.WriteLine(GetBooksByCategory(dbContext, input));
    }

    public static string GetBooksByCategory(BookShopContext context, string input)
    {
        string[] categories = input.ToLower().Split(" ", StringSplitOptions.RemoveEmptyEntries);

        string[] titles = context.Books
            .AsNoTracking()
            .Where(b => b.BookCategories.Any(bc => categories.Contains(bc.Category.Name.ToLower())))
            .OrderBy(b => b.Title)
            .Select(b => b.Title)
            .ToArray();

        return string.Join(Environment.NewLine, titles);
    }
}