using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
    }

    public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.Salary > 50_000)
            .Select(
                e =>
                new
                {
                    e.FirstName,
                    e.Salary
                })
            .OrderBy(e => e.FirstName)
            .ToList();

        StringBuilder sb = new();
        foreach (var e in employees) sb.AppendLine($"{e.FirstName} - {e.Salary:F2}");

        return sb.ToString().Trim();
    }
}