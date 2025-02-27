using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using static Boardgames.Data.DataConstraints;

namespace Boardgames.DataProcessor.ImportDto;

public class ImportSellerJsonDto
{
    [Required]
    [MinLength(SellerNameMinLength)]
    [MaxLength(SellerNameMaxLength)]
    [JsonPropertyName(nameof(Name))]
    public string Name { get; set; } = null!;

    [Required]
    [JsonPropertyName(nameof(Address))]
    [MinLength(SellerAddressMinLength)]
    [MaxLength(SellerAddressMaxLength)]
    public string Address { get; set; } = null!;

    [Required]
    [JsonPropertyName(nameof(Country))]
    public string Country { get; set; } = null!;

    [Required]
    [JsonPropertyName(nameof(Website))]
    [RegularExpression(SellerWebsitePattern)]
    public string Website { get; set; } = null!;

    [JsonPropertyName(nameof(Boardgames))]
    public int[] Boardgames { get; set; } = null!;
}