using Boardgames.Data.Models;
using Boardgames.Data.Models.Enums;
using Boardgames.DataProcessor.ImportDto;
using System.Text;
using System.Text.Json;
using System.Xml.Serialization;

namespace Boardgames.DataProcessor;

using Data;
using System.ComponentModel.DataAnnotations;

public class Deserializer
{
    private const string ErrorMessage = "Invalid data!";

    private const string SuccessfullyImportedCreator
        = "Successfully imported creator – {0} {1} with {2} boardgames.";

    private const string SuccessfullyImportedSeller
        = "Successfully imported seller - {0} with {1} boardgames.";

    public static string ImportCreators(BoardgamesContext context, string xmlString)
    {
        ImportCreatorXmlDto[] creatorDtos = DeserializeXml<ImportCreatorXmlDto[]>(xmlString, "Creators");
        StringBuilder sb = new();
        ICollection<Creator> creators = new HashSet<Creator>();

        foreach (ImportCreatorXmlDto c in creatorDtos)
        {
            if (!IsValid(c))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            ICollection<Boardgame> validBoardgames = new HashSet<Boardgame>();
            foreach (ImportBoardgameXmlDto b in c.Boardgames)
            {
                if (!IsValid(b))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validBoardgames.Add(new Boardgame
                {
                    Name = b.Name,
                    Rating = b.Rating,
                    YearPublished = b.YearPublished,
                    CategoryType = (CategoryType)b.CategoryType,
                    Mechanics = b.Mechanics
                });
            }

            creators.Add(new Creator
            {
                FirstName = c.FirstName,
                LastName = c.LastName,
                Boardgames = validBoardgames
            });

            sb.AppendLine(string.Format(SuccessfullyImportedCreator, c.FirstName, c.LastName, validBoardgames.Count));
        }

        context.Creators.AddRange(creators);
        context.SaveChanges();

        return sb.ToString().Trim();
    }

    public static string ImportSellers(BoardgamesContext context, string jsonString)
    {
        ImportSellerJsonDto[] sellerDtos = DeserializeJson<ImportSellerJsonDto[]>(jsonString);
        StringBuilder sb = new();
        ICollection<Seller> sellers = new HashSet<Seller>();

        foreach (ImportSellerJsonDto s in sellerDtos)
        {
            if (!IsValid(s))
            {
                sb.AppendLine(ErrorMessage);
                continue;
            }

            ICollection<BoardgameSeller> validBoardgamesSellers = new HashSet<BoardgameSeller>();
            foreach (int bi in s.Boardgames.Distinct())
            {
                if (!context.Boardgames.Any(b => b.Id == bi))
                {
                    sb.AppendLine(ErrorMessage);
                    continue;
                }

                validBoardgamesSellers.Add(new BoardgameSeller
                {
                    BoardgameId = bi,
                });
            }

            sellers.Add(new Seller
            {
                Name = s.Name,
                Address = s.Address,
                Country = s.Country,
                Website = s.Website,
                BoardgamesSellers = validBoardgamesSellers
            });

            sb.AppendLine(string.Format(SuccessfullyImportedSeller, s.Name, validBoardgamesSellers.Count));
        }

        context.Sellers.AddRange(sellers);
        context.SaveChanges();

        return sb.ToString().Trim();
    }

    private static bool IsValid(object dto)
    {
        var validationContext = new ValidationContext(dto);
        var validationResult = new List<ValidationResult>();

        return Validator.TryValidateObject(dto, validationContext, validationResult, true);
    }

    private static T DeserializeXml<T>(string xmlString, string rootElementName)
    {
        XmlSerializer serializer = new(typeof(T), new XmlRootAttribute(rootElementName));
        using StringReader sr = new(xmlString);

        return (T)serializer.Deserialize(sr)!;
    }

    private static T DeserializeJson<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString)!;
}