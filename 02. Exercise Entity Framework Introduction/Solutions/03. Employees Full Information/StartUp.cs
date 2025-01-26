using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetEmployeesFullInformation(context));
    }

    public static string GetEmployeesFullInformation(SoftUniContext context)
    {
        var employees = context.Employees
            .OrderBy(e => e.EmployeeId)
            .Select(
                e =>
                new
                {
                    e.FirstName,
                    e.LastName,
                    e.MiddleName,
                    e.JobTitle,
                    e.Salary
                })
            .ToList();

        StringBuilder sb = new();
        foreach (var e in employees) sb.AppendLine($"{e.FirstName} {e.LastName} {e.MiddleName} {e.JobTitle} {e.Salary:F2}");

        return sb.ToString().Trim();
    }
}