﻿using System.ComponentModel.DataAnnotations;

namespace AcademicRecordsApp.Data.Models
{
    public class Student
    {
        public Student()
        {
            Grades = new HashSet<Grade>();
            Courses = new HashSet<Course>();
        }

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string FullName { get; set; } = null!;

        public ICollection<Grade> Grades { get; set; }

        public ICollection<Course> Courses { get; set; }
    }
}
