using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(AddNewAddressToEmployee(context));
    }

    public static string AddNewAddressToEmployee(SoftUniContext context)
    {
        Address address = new()
        {
            AddressText = "Vitoshka 15",
            TownId = 4
        };


        Employee employee = context.Employees
            .FirstOrDefault(e => e.LastName == "Nakov")!;

        employee.Address = address;
        context.SaveChanges();

        var employees = context.Employees
            .OrderByDescending(e => e.Address.AddressId)
            .Take(10)
            .Select(e => e.Address)
            .ToList();

        StringBuilder sb = new();
        foreach (var e in employees) sb.AppendLine(e.AddressText);

        return sb.ToString().Trim();
    }
}