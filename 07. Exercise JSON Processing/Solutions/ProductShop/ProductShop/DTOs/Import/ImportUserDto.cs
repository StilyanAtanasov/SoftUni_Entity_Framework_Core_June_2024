using System.Text.Json.Serialization;

namespace ProductShop.DTOs.Import;

public class ImportUserDto
{
    [JsonPropertyName("firstName")]
    public string? FirstName { get; set; }

    [JsonPropertyName("lastName")]
    public string LastName { get; set; } = null!;

    [JsonPropertyName("age")]
    public int? Age { get; set; }
}