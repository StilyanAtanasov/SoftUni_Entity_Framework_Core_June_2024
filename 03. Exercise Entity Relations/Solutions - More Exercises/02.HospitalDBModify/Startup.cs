using Microsoft.EntityFrameworkCore;
using P01_HospitalDatabase.Data;
using P01_HospitalDatabase.Data.Models;
using System.Linq.Expressions;
using System.Text;

namespace P01_HospitalDatabase;

public class Startup
{
    private readonly HospitalContext _context;

    public Startup() => _context = new HospitalContext();

    public void Run()
    {
        PrintMenu();

        string command;
        while ((command = Console.ReadLine()!) != "exit")
        {
            switch (command)
            {
                case "1":
                    Console.WriteLine(PrintPatients());
                    break;
                case "2":
                    PrintMedicaments();
                    break;
                case "3":
                    PrintDiagnoses();
                    break;
                case "4":
                    PrintVisitations();
                    break;
                case "5":
                    PrintDoctors();
                    break;
                case "6":
                    AddPatient();
                    break;
                case "7":
                    AddMedicament();
                    break;
                case "8":
                    AddDiagnose();
                    break;
                case "9":
                    AddVisitation();
                    break;
                case "10":
                    AddDoctor();
                    break;
                case "11":
                    Prescribe();
                    break;
                case "12":
                    FindPatient();
                    break;
                case "13":
                    FindMedicament();
                    break;
                case "14":
                    FindDoctor();
                    break;
            }

            PrintMenu();
        }
    }

    private void FindDoctor()
    {
        Console.Write("Enter first name: ");
        string name = Console.ReadLine()!.ToLower();

        PrintDoctors(d => d.Name.ToLower() == name);
    }

    private void FindPatient()
    {
        Console.Write("Enter first name: ");
        string firstName = Console.ReadLine()!.ToLower();

        Console.Write("Enter last name: ");
        string lastName = Console.ReadLine()!.ToLower();

        string result = PrintPatients(p => p.FirstName.ToLower() == firstName && (string.IsNullOrEmpty(lastName) || p.LastName.ToLower() == lastName));
        Console.WriteLine(string.IsNullOrEmpty(result) ? $"No such person {firstName} {lastName} found!" : result);
    }

    private void FindMedicament()
    {
        Console.Write("Enter name: ");
        string name = Console.ReadLine()!.ToLower();

        PrintMedicaments(m => m.Name.ToLower() == name);
    }

    private void PrintVisitations()
    {
        var visitations = _context.Visitations.ToList();

        Console.WriteLine("----------- Visitations -----------");
        foreach (var v in visitations)
            Console.WriteLine($"Id: {v.VisitationId} | Date: {v.Date} | Comments: {v.Comments} | PatientId: {v.PatientId} | DoctorId {v.DoctorId}");
    }

    private void PrintMedicaments(Expression<Func<Medicament, bool>>? condition = null)
    {
        condition ??= p => true;

        var medicaments = _context.Medicaments
            .Where(condition)
            .ToList();

        Console.WriteLine("----------- Medicaments -----------");
        if (medicaments.Count == 0)
        {
            Console.WriteLine("No medicaments found!");
            return;
        }

        foreach (var m in medicaments)
            Console.WriteLine($"Id: {m.MedicamentId} | Name: {m.Name}");
    }

    private void PrintDiagnoses()
    {
        var diagnoses = _context.Diagnoses.ToList();

        Console.WriteLine("----------- Diagnoses -----------");
        foreach (var d in diagnoses)
            Console.WriteLine($"Id: {d.DiagnoseId} | Name: {d.Name} | Comments: {d.Comments} | PatientId: {d.PatientId}");
    }

    private void PrintDoctors(Expression<Func<Doctor, bool>>? condition = null)
    {
        condition ??= p => true;
        var doctors = _context.Doctors.Where(condition).ToList();

        Console.WriteLine("----------- Doctors -----------");
        if (doctors.Count == 0)
        {
            Console.WriteLine("No medicaments found!");
            return;
        }

        foreach (var d in doctors)
            Console.WriteLine($"Id: {d.DoctorId} | Name: {d.Name} | Comments: {d.Specialty}");
    }

    private string PrintPatients(Expression<Func<Patient, bool>>? condition = null)
    {
        condition ??= p => true;

        var patients = _context.Patients
            .Where(condition)
            .Include(p => p.Visitations)
            .Include(p => p.Diagnoses)
            .Include(p => p.Prescriptions)
                .ThenInclude(pr => pr.Medicament)
            .ToList();

        StringBuilder sb = new();
        foreach (var patient in patients)
        {
            sb.AppendLine($"Id: {patient.PatientId} | Name: {patient.FirstName} {patient.LastName} | Address: {patient.Address} | Email: {patient.Email} | HasInsurance: {patient.HasInsurance}");

            sb.AppendLine("  -- Visitations: ");
            sb.AppendLine(patient.Visitations.Any()
                ? string.Join("\n", patient.Visitations.Select(v => $"     Date: {v.Date} | Comments: {v.Comments}"))
                : "     - No visitations.");

            sb.AppendLine("  -- Diagnoses: ");
            sb.AppendLine(patient.Diagnoses.Any()
                ? string.Join("\n", patient.Diagnoses.Select(d => $"       DiagnoseId: {d.DiagnoseId} | Name: {d.Name} | Comments: {d.Comments}"))
                : "     - No diagnoses.");

            sb.AppendLine("  -- Prescriptions: ");
            sb.AppendLine(patient.Prescriptions.Any()
                ? string.Join("\n", patient.Prescriptions.Select(p => $"       MedicamentId: {p.MedicamentId} | Name: {p.Medicament.Name}"))
                : "     - No prescriptions.");
        }

        return sb.ToString().Trim();
    }

    private void AddPatient()
    {
        try
        {
            Console.Write("Enter first name: ");
            string firstName = Console.ReadLine()!;

            Console.Write("Enter last name: ");
            string lastName = Console.ReadLine()!;

            Console.Write("Enter address: ");
            string address = Console.ReadLine()!;

            Console.Write("Enter email: ");
            string email = Console.ReadLine()!;

            Console.Write("Has insurance? (yes/no): ");
            bool hasInsurance = Console.ReadLine()!.Trim().ToLower() == "yes";

            if (string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(address) || string.IsNullOrEmpty(email))
                throw new ArgumentException("Values cannot be NULL !");

            var patient = new Patient
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address,
                Email = email,
                HasInsurance = hasInsurance
            };

            _context.Patients.Add(patient);
            _context.SaveChanges();

            Console.WriteLine("Patient added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding patient!");
            Console.WriteLine(ex.Message);
        }
    }

    private void AddDiagnose()
    {
        try
        {
            Console.Write("Enter name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter comments: ");
            string comments = Console.ReadLine()!;

            Console.Write("Enter patient Id: ");
            int patientId = int.Parse(Console.ReadLine()!);

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(comments))
                throw new ArgumentException("Values cannot be NULL !");

            var diagnose = new Diagnose()
            {
                Name = name,
                Comments = comments,
                PatientId = patientId
            };

            _context.Diagnoses.Add(diagnose);
            _context.SaveChanges();

            Console.WriteLine("Diagnose added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding diagnose!");
            Console.WriteLine(ex.Message);
        }
    }

    private void AddMedicament()
    {
        try
        {
            Console.Write("Enter medicament name: ");
            string name = Console.ReadLine()!;

            if (string.IsNullOrEmpty(name))
                throw new ArgumentException("Name cannot be NULL !");

            var medicament = new Medicament
            {
                Name = name
            };

            _context.Medicaments.Add(medicament);
            _context.SaveChanges();

            Console.WriteLine($"{name} added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding medicament!");
            Console.WriteLine(ex.Message);
        }
    }

    private void AddVisitation()
    {
        try
        {
            Console.Write("Enter comments: ");
            string comments = Console.ReadLine()!;

            Console.Write("Enter patientId: ");
            int patientId = int.Parse(Console.ReadLine()!);

            if (string.IsNullOrEmpty(comments))
                throw new ArgumentException("Comments cannot be NULL !");

            var visitation = new Visitation()
            {
                Date = DateTime.Now,
                Comments = comments,
                PatientId = patientId
            };

            _context.Visitations.Add(visitation);
            _context.SaveChanges();

            Console.WriteLine("Visitation added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding visitation!");
            Console.WriteLine(ex.Message);
        }
    }

    private void AddDoctor()
    {
        try
        {
            Console.Write("Enter doctor name: ");
            string name = Console.ReadLine()!;

            Console.Write("Enter doctor specialty: ");
            string specialty = Console.ReadLine()!;

            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(specialty))
                throw new ArgumentException("Cannot have NULL values!");

            var doctor = new Doctor()
            {
                Name = name,
                Specialty = specialty
            };

            _context.Doctors.Add(doctor);
            _context.SaveChanges();

            Console.WriteLine($"Dr. {name} added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding doctor!");
            Console.WriteLine(ex.Message);
        }
    }

    private void Prescribe()
    {
        try
        {
            Console.Write("Enter patientId: ");
            int patientId = int.Parse(Console.ReadLine()!);

            Console.Write("Enter medicamentId: ");
            int medicamentId = int.Parse(Console.ReadLine()!);

            var patient = _context.Patients.Find(patientId);
            var medicament = _context.Medicaments.Find(medicamentId);

            if (patient == null || medicament == null)
                throw new ArgumentException("Invalid Patient or Medicament ID.");

            bool alreadyPrescribed = _context.PatientsMedicaments
            .Any(pm => pm.PatientId == patientId && pm.MedicamentId == medicamentId);

            if (alreadyPrescribed)
                throw new ArgumentException("This medicament is already prescribed to the patient.");

            var prescription = new PatientMedicament
            {
                PatientId = patientId,
                MedicamentId = medicamentId
            };

            _context.PatientsMedicaments.Add(prescription);
            _context.SaveChanges();

            Console.WriteLine("Prescription added successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error adding prescription!");
            Console.WriteLine(ex.Message);
        }
    }

    private void PrintMenu()
    {
        Console.WriteLine();
        Console.WriteLine(" -------- Menu --------");
        Console.WriteLine("1: View All Patients");
        Console.WriteLine("2: View All Medicaments");
        Console.WriteLine("3: View All Diagnoses");
        Console.WriteLine("4: View All Visitations");
        Console.WriteLine("5: View All Doctors");
        Console.WriteLine("6: Add Patient");
        Console.WriteLine("7: Add Medicament");
        Console.WriteLine("8: Add Diagnoses");
        Console.WriteLine("9: Add Visitations");
        Console.WriteLine("10: Add Doctor");
        Console.WriteLine("11: Prescribe");
        Console.WriteLine("12: Find Patients");
        Console.WriteLine("13: Find Medicament");
        Console.WriteLine("14: Find Doctor");
        Console.WriteLine("Type 'exit' to leave the application!");
        Console.WriteLine(" ----------------------");
        Console.WriteLine();
    }

    public static void Main(string[] args) => new Startup().Run();
}