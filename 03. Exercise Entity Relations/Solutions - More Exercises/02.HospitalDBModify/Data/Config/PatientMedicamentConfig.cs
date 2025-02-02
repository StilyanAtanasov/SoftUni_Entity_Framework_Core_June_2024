using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data.Config;

public class PatientMedicamentConfig : IEntityTypeConfiguration<PatientMedicament>
{
    public void Configure(EntityTypeBuilder<PatientMedicament> builder)
    {
        builder
            .HasKey(pm => new { pm.PatientId, pm.MedicamentId });
    }
}