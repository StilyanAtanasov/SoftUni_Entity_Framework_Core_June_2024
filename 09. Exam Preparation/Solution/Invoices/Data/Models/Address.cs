using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Invoices.Data.DataConstraints;

namespace Invoices.Data.Models;

public class Address
{
    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(AddressStreetNameMaxLength)]
    public string StreetName { get; set; } = null!;

    [Required]
    public int StreetNumber { get; set; }

    [Required]
    public string PostCode { get; set; } = null!;

    [Required]
    [MaxLength(AddressCityMaxLength)]
    public string City { get; set; } = null!;

    [Required]
    [MaxLength(AddressCountryMaxLength)]
    public string Country { get; set; } = null!;

    [Required]
    [ForeignKey(nameof(Client))]
    public int ClientId { get; set; }

    public Client Client { get; set; } = null!;
}