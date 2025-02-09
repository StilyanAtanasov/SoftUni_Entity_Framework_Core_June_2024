using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AcademicRecordsApp.Data.Models
{
    public class Exam
    {
        public Exam() => Grades = new HashSet<Grade>();

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [Required]
        [ForeignKey(nameof(Course))]
        public int? CourseId { get; set; }

        public Course Course { get; set; } = null!;

        public ICollection<Grade> Grades { get; set; }
    }
}
