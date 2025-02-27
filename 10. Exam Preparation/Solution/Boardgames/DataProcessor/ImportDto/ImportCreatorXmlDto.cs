using Boardgames.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.Xml.Serialization;
using static Boardgames.Data.DataConstraints;

namespace Boardgames.DataProcessor.ImportDto;

[XmlType(nameof(Creator))]
public class ImportCreatorXmlDto
{
    [XmlElement(nameof(FirstName))]
    [MinLength(CreatorFirstNameMinLength)]
    [MaxLength(CreatorFirstNameMaxLength)]
    public string FirstName { get; set; } = null!;


    [XmlElement(nameof(LastName))]
    [MinLength(CreatorLastNameMinLength)]
    [MaxLength(CreatorLastNameMaxLength)]
    public string LastName { get; set; } = null!;

    [XmlArray(nameof(Boardgames))]
    public ImportBoardgameXmlDto[] Boardgames { get; set; } = null!;
}