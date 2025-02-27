using Boardgames.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Boardgames.Data.DataConstraints;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Boardgame))]
public class ImportBoardgameXmlDto
{
    [XmlElement(nameof(Name))]
    [MinLength(BoardGameNameMinLength)]
    [MaxLength(BoardGameNameMaxLength)]
    public string Name { get; set; } = null!;

    [XmlElement(nameof(Rating))]
    [Range(BoardGameMinRating, BoardGameMaxRating)]
    public double Rating { get; set; }

    [XmlElement(nameof(YearPublished))]
    [Range(BoardGameMinYearPublished, BoardGameMaxYearPublished)]
    public int YearPublished { get; set; }

    [XmlElement(nameof(CategoryType))]
    [Range(BoardGameMinCategoryType, BoardGameMaxCategoryType)]
    public int CategoryType { get; set; }

    [XmlElement(nameof(Mechanics))]
    public string Mechanics { get; set; } = null!;
}