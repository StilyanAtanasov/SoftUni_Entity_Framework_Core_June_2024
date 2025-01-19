using MiniORM.App.Data.Entities;

string connectionString = "Server=.;Database=MiniORM;Trusted_Connection=True;TrustServerCertificate=True";
SoftUniDbContext dbContext = new(connectionString);

dbContext.Employees.Add(new Employee
{
    FirstName = "Gosho",
    LastName = "Inserted",
    DepartmentId = dbContext.Departments.First().Id,
    IsEmployed = true
});

var employee = dbContext.Employees.Last();
employee.FirstName = "Modified";

dbContext.SaveChanges();