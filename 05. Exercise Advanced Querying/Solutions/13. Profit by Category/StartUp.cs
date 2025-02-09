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

        Console.WriteLine(GetTotalProfitByCategory(dbContext));
    }

    public static string GetTotalProfitByCategory(BookShopContext context)
    {
        var profitByCategory = context.Categories
            .AsNoTracking()
            .Select(c => new
            {
                c.Name,
                TotalProfit = c.CategoryBooks.Sum(b => b.Book.Copies * b.Book.Price)
            })
            .OrderByDescending(c => c.TotalProfit)
            .ThenBy(c => c.Name)
            .ToArray();

        StringBuilder sb = new();
        foreach (var pbc in profitByCategory) sb.AppendLine($"{pbc.Name} ${pbc.TotalProfit:F2}");

        return sb.ToString().Trim();
    }
}