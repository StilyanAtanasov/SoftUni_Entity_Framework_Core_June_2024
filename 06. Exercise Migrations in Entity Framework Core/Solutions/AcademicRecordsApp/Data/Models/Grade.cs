namespace AcademicRecordsApp.Data.Models
{
    public class Grade
    {
        public int Id { get; set; }
        public decimal Value { get; set; }
        public int ExamId { get; set; }
        public int StudentId { get; set; }

        public Exam Exam { get; set; } = null!;
        public Student Student { get; set; } = null!;
    }
}
