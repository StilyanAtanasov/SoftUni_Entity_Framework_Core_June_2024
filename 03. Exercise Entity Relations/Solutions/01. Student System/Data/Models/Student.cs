namespace P01_StudentSystem.Data.Models;

public class Student
{
    public int StudentId { get; set; }
    public string Name { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public DateTime RegisteredOn { get; set; }
    public DateTime? Birthday { get; set; }
    public ICollection<Homework> Homeworks { get; set; } = new List<Homework>();
    public ICollection<StudentCourse> StudentsCourses { get; set; } = new List<StudentCourse>();
}