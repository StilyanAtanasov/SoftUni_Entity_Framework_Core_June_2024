using SoftUni.Data;
using SoftUni.Models;
using System.Text;

namespace SoftUni;

public class StartUp
{
    public static void Main()
    {
        SoftUniContext context = new();
        Console.WriteLine(DeleteProjectById(context));
    }

    public static string DeleteProjectById(SoftUniContext context)
    {
        EmployeeProject[] employeesProjectsToRemove = context.EmployeesProjects.Where(ep => ep.ProjectId == 2).ToArray();
        foreach (EmployeeProject ep in employeesProjectsToRemove) context.Remove(ep);

        var projectToRemove = context.Projects.Find(2)!;
        context.Remove(projectToRemove);

        context.SaveChanges();

        var projects = context.Projects
            .Take(10)
            .Select(p => p.Name)
            .ToList();

        StringBuilder sb = new();
        foreach (var p in projects) sb.AppendLine(p);

        return sb.ToString().Trim();
    }
}