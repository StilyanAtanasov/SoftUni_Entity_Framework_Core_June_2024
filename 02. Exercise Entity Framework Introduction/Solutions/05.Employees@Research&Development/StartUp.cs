using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
    }

    public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
    {
        var employees = context.Employees
            .Where(e => e.Department.Name == "Research and Development")
            .Select(
                e =>
                new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
            .OrderBy(e => e.Salary)
            .ThenByDescending(e => e.FirstName)
            .ToList();

        StringBuilder sb = new();
        foreach (var e in employees) sb.AppendLine($"{e.FirstName} {e.LastName} from {e.DepartmentName} - ${e.Salary:F2}");

        return sb.ToString().Trim();
    }
}