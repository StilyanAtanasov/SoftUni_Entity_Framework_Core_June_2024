using System.ComponentModel.DataAnnotations;

namespace AcademicRecordsApp.Data.Models
{
    public class Course
    {
        public Course()
        {
            Students = new HashSet<Student>();
            Exams = new HashSet<Exam>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        public ICollection<Exam> Exams { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
