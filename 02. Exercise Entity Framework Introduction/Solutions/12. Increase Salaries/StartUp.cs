using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(IncreaseSalaries(context));
    }

    public static string IncreaseSalaries(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => new[] { "Engineering", "Tool Design", "Marketing", "Information Services" }.Contains(e.Department.Name))
            .OrderBy(e => e.FirstName).ThenBy(e => e.LastName)
            .ToList();

        StringBuilder sb = new();
        foreach (var e in employees) sb.AppendLine($"{e.FirstName} {e.LastName} (${e.Salary *= 1.12m:F2})");

        context.SaveChanges();
        return sb.ToString().Trim();
    }
}