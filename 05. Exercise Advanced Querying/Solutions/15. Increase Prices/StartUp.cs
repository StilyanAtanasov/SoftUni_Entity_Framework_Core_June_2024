using BookShop.Initializer;

namespace BookShop;

using Data;

public class StartUp
{
    public static void Main()
    {
        using var dbContext = new BookShopContext();
        DbInitializer.ResetDatabase(dbContext);

        IncreasePrices(dbContext);
    }

    public static void IncreasePrices(BookShopContext context)
    {
        var books = context.Books
            .Where(b => b.ReleaseDate!.Value.Year < 2010)
            .ToArray();

        foreach (var book in books) book.Price += 5;

        context.SaveChanges();
    }
}