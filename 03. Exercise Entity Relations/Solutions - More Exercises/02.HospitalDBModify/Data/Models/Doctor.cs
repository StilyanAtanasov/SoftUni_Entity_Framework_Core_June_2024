using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models;

public class Doctor
{
    [Key]
    public int DoctorId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string Name { get; set; } = null!;

    [Required]
    [Column(TypeName = "NVARCHAR(100)")]
    public string Specialty { get; set; } = null!;

    public ICollection<Visitation> Visitations { get; set; } = new HashSet<Visitation>();
}