using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetDepartmentsWithMoreThan5Employees(context));
    }

    public static string GetDepartmentsWithMoreThan5Employees(SoftUniContext context)
    {
        var departments = context.Departments
            .Where(d => d.Employees.Count > 5)
            .OrderBy(d => d.Employees.Count)
            .ThenBy(d => d.Name)
            .Select(d => new
            {
                DepartmentName = d.Name,
                ManagerName = d.Manager.FirstName + " " + d.Manager.LastName,
                Employees = d.Employees
                    .OrderBy(e => e.FirstName)
                    .ThenBy(e => e.LastName)
                    .Select(e => new
                    {
                        EmployeeName = e.FirstName + " " + e.LastName,
                        EmployeeJob = e.JobTitle
                    })
                    .ToArray()
            })
            .ToList();

        StringBuilder sb = new();

        foreach (var d in departments)
        {
            sb.AppendLine($"{d.DepartmentName} - {d.ManagerName}");
            foreach (var e in d.Employees) sb.AppendLine($"{e.EmployeeName} - {e.EmployeeJob}");
        }

        return sb.ToString().Trim();
    }
}