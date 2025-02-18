namespace CarDealer.DTOs.Import;

public class ImportCarDto
{
    public ImportCarDto()
    {
        PartsId = new HashSet<int>();
    }

    public string Make { get; set; } = null!;

    public string Model { get; set; } = null!;

    public long TraveledDistance { get; set; }

    public ICollection<int> PartsId { get; set; }
}