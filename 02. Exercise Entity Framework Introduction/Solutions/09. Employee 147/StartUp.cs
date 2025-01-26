using Microsoft.EntityFrameworkCore;
using SoftUni.Data;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(GetEmployee147(context));
    }

    public static string GetEmployee147(SoftUniContext context)
    {
        var employee = context.Employees
            .Include(e => e.EmployeesProjects)
            .ThenInclude(ep => ep.Project)
            .First(e => e.EmployeeId == 147);

        StringBuilder sb = new();
        sb.AppendLine($"{employee.FirstName} {employee.LastName} - {employee.JobTitle}");

        foreach (var ep in employee.EmployeesProjects.OrderBy(ep => ep.Project.Name))
            sb.AppendLine(ep.Project.Name);

        return sb.ToString().Trim();
    }
}