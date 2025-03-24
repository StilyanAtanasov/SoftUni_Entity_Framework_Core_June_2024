using System.ComponentModel.DataAnnotations;
using static TravelAgency.Data.Constraints.EntityConstraints.Customer;

namespace TravelAgency.Data.Models;

public class Customer
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(FullNameMaxLength)]
    public string FullName { get; set; } = null!;

    [Required]
    [MaxLength(EmailMaxLength)]
    public string Email { get; set; } = null!;

    [Required]
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Booking> Bookings { get; set; } = new HashSet<Booking>();
}