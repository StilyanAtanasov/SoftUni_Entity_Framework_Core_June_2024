using Boardgames.Data.Models;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor.ExportDto;

[XmlType(nameof(Creator))]
public class ExportCreatorXmlDto
{
    [XmlElement(nameof(CreatorName))]
    public string CreatorName { get; set; } = null!;

    [XmlArray(nameof(Boardgames))]
    public ExportBoardgameXmlDto[] Boardgames { get; set; } = null!;

    [XmlAttribute(nameof(BoardgamesCount))]
    public int BoardgamesCount { get; set; }
}