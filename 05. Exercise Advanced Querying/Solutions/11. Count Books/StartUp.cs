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

        int lengthCheck = int.Parse(Console.ReadLine()!);
        Console.WriteLine(CountBooks(dbContext, lengthCheck));
    }

    public static int CountBooks(BookShopContext context, int lengthCheck)
    {
        int booksCount = context.Books
            .AsNoTracking()
            .Count(b => b.Title.Length > lengthCheck);

        return booksCount;
    }
}