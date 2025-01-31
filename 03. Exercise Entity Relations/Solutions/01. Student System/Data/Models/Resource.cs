using ResourceType = P01_StudentSystem.Data.Enums.ResourceType;

namespace P01_StudentSystem.Data.Models;

public class Resource
{
    public int ResourceId { get; set; }
    public string Name { get; set; } = null!;
    public string Url { get; set; } = null!;
    public ResourceType ResourceType { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}