using Boardgames.DataProcessor.ExportDto;
using System.Text.Json;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor;

using Data;

public class Serializer
{
    private static readonly JsonSerializerOptions ExportJsonOptions = new()
    {
        WriteIndented = true
    };

    public static string ExportCreatorsWithTheirBoardgames(BoardgamesContext context)
    {
        ExportCreatorXmlDto[] creators = context.Creators
            .AsEnumerable()
            .Where(c => c.Boardgames.Any())
            .Select(c => new ExportCreatorXmlDto
            {
                CreatorName = $"{c.FirstName} {c.LastName}",
                Boardgames = c.Boardgames
                    .Select(b => new ExportBoardgameXmlDto
                    {
                        BoardgameName = b.Name,
                        BoardgameYearPublished = b.YearPublished,
                    })
                    .OrderBy(b => b.BoardgameName)
                    .ToArray(),
                BoardgamesCount = c.Boardgames.Count
            })
            .OrderByDescending(c => c.BoardgamesCount)
            .ThenBy(c => c.CreatorName)
            .ToArray();

        return SerializeXml(creators, "Creators");
    }

    public static string ExportSellersWithMostBoardgames(BoardgamesContext context, int year, double rating)
    {
        ExportSellerJsonDto[] sellers = context.Sellers
            .Where(s => s.BoardgamesSellers.Any(bs => bs.Boardgame.YearPublished >= year
                                                      && bs.Boardgame.Rating <= rating))
            .Select(s => new ExportSellerJsonDto
            {
                Name = s.Name,
                Website = s.Website,
                Boardgames = s.BoardgamesSellers
                    .Where(bs => bs.Boardgame.YearPublished >= year && bs.Boardgame.Rating <= rating)
                    .Select(bs => new ExportBoardgameJsonDto
                    {
                        Name = bs.Boardgame.Name,
                        Rating = bs.Boardgame.Rating,
                        Mechanics = bs.Boardgame.Mechanics,
                        Category = bs.Boardgame.CategoryType.ToString()
                    })
                    .OrderByDescending(b => b.Rating)
                    .ThenBy(b => b.Name)
                    .ToArray()
            })
            .OrderByDescending(s => s.Boardgames.Length)
            .ThenBy(s => s.Name)
            .Take(5)
            .ToArray();

        return SerializeJson(sellers);
    }

    private static string SerializeXml<T>(T dto, string rootElementName)
    {
        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElementName));
        using StringWriter sw = new();

        XmlSerializerNamespaces xmlns = new();
        xmlns.Add("", "");

        serializer.Serialize(sw, dto, xmlns);
        return sw.ToString();
    }

    private static string SerializeJson<T>(T dto) => JsonSerializer.Serialize(dto, ExportJsonOptions);
}