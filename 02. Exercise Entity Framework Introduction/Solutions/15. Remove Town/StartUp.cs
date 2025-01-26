using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(RemoveTown(context));
    }

    public static string RemoveTown(SoftUniContext context)
    {
        int[] addressIds = context.Addresses
            .Where(a => a.Town.Name == "Seattle")
            .Select(a => a.AddressId)
            .ToArray();

        Employee[] employees = context.Employees.Where(e => addressIds.Contains(e.AddressId ?? -1)).ToArray();
        foreach (Employee ep in employees) ep.AddressId = null;

        Address[] addresses = context.Addresses.Where(a => addressIds.Contains(a.AddressId)).ToArray();

        int removedCount = 0;
        foreach (Address a in addresses)
        {
            context.Remove(a);
            removedCount++;
        }

        Town townToRemove = context.Towns.First(t => t.Name == "Seattle");
        context.Remove(townToRemove);

        context.SaveChanges();

        return $"{removedCount} addresses in Seattle were deleted";
    }
}