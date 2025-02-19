using System.Xml.Serialization;

namespace ProductShop.DTOs.Export;

public class ExportUsersDto
{
    public ExportUsersDto()
    {
        Users = new HashSet<UserSellers>();
    }

    [XmlElement("count")]
    public int UsersCount { get; set; }

    [XmlArray("users")]
    public HashSet<UserSellers> Users { get; set; }
}