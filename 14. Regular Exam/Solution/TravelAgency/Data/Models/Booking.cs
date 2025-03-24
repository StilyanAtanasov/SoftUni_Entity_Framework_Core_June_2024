using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TravelAgency.Data.Models;

public class Booking
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime BookingDate { get; set; }

    [Required]
    public int CustomerId { get; set; }

    public Customer Customer { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(TourPackage))]
    public int TourPackageId { get; set; }

    public TourPackage TourPackage { get; set; } = null!;
}