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

        string input = Console.ReadLine()!;
        Console.WriteLine(GetBooksByAuthor(dbContext, input));
    }

    public static string GetBooksByAuthor(BookShopContext context, string input)
    {
        var books = context.Books
             .AsNoTracking()
             .Where(b => b.Author.LastName.ToLower().StartsWith(input.ToLower()))
             .OrderBy(b => b.BookId)
             .Select(b => new
             {
                 b.Title,
                 AuthorName = $"{b.Author.FirstName} {b.Author.LastName}"
             })
             .ToArray();

        StringBuilder sb = new();
        foreach (var book in books) sb.AppendLine($"{book.Title} ({book.AuthorName})");

        return sb.ToString().Trim();
    }
}