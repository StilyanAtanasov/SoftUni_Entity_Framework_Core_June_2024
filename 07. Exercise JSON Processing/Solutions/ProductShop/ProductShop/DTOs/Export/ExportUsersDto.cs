namespace ProductShop.DTOs.Export;

public class ExportUsersDto
{
    public ExportUsersDto()
    {
        Users = new HashSet<UserSellers>();
    }

    public int UsersCount { get; set; }

    public ICollection<UserSellers> Users { get; set; }
}