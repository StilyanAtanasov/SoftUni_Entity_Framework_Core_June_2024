using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data.Config;
using P01_HospitalDatabase.Data.Models;

namespace P01_HospitalDatabase.Data;

public class HospitalContext : DbContext
{
    public HospitalContext() { }

    public HospitalContext(DbContextOptions<HospitalContext> options) : base(options) { }

    public DbSet<Patient> Patients { get; set; } = null!;
    public DbSet<Visitation> Visitations { get; set; } = null!;
    public DbSet<Diagnose> Diagnoses { get; set; } = null!;
    public DbSet<Medicament> Medicaments { get; set; } = null!;
    public DbSet<PatientMedicament> PatientsMedicaments { get; set; } = null!;
    public DbSet<Doctor> Doctors { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PatientMedicamentConfig());

        base.OnModelCreating(modelBuilder);
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=.;Database=Hospital;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}