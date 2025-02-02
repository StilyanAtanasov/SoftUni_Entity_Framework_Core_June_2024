using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace P01_HospitalDatabase.Data.Models;

public class Medicament
{
    [Key]
    public int MedicamentId { get; set; }

    [Required]
    [Column(TypeName = "NVARCHAR(50)")]
    public string Name { get; set; } = null!;

    public ICollection<PatientMedicament> Prescriptions { get; set; } = new HashSet<PatientMedicament>();
}