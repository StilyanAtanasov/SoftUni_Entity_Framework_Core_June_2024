using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetAddressesByTown(context));
    }

    public static string GetAddressesByTown(SoftUniContext context)
    {
        var addresses = context.Addresses
            .OrderByDescending(a => a.Employees.Count)
            .ThenBy(a => a.Town.Name)
            .ThenBy(a => a.AddressText)
            .Take(10)
            .Select(a => new
            {
                a.AddressText,
                AddressTown = a.Town.Name,
                EmployeeCount = a.Employees.Count
            })
            .ToList();

        StringBuilder sb = new();
        foreach (var a in addresses)
            sb.AppendLine($"{a.AddressText}, {a.AddressTown} - {a.EmployeeCount} employees");

        return sb.ToString().Trim();
    }
}