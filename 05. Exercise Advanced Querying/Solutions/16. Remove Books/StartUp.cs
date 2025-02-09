using BookShop.Initializer;

namespace BookShop;

using Data;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        Console.WriteLine(RemoveBooks(dbContext));
    }

    public static int RemoveBooks(BookShopContext context)
    {
        var books = context.Books
            .Where(b => b.Copies < 4200)
            .ToList();

        context.RemoveRange(books);

        context.SaveChanges();
        return books.Count;
    }
}