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
        Console.WriteLine(GetAuthorNamesEndingIn(dbContext, input));
    }

    public static string GetAuthorNamesEndingIn(BookShopContext context, string input)
    {
        string[] authors = context.Authors
            .AsNoTracking()
            .Where(a => a.FirstName.EndsWith(input))
            .Select(a => $"{a.FirstName} {a.LastName}")
            .ToArray();

        return string.Join(Environment.NewLine, authors.OrderBy(a => a));
    }
}