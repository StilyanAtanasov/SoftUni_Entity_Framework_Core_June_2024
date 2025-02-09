using System.Globalization;
using System.Text;

namespace MusicHub;

using Data;
using Initializer;

public class StartUp
{
    public static void Main()
    {
        MusicHubDbContext context = new MusicHubDbContext();

        DbInitializer.ResetDatabase(context);

        Console.WriteLine(ExportAlbumsInfo(context, 9));
    }

    public static string ExportAlbumsInfo(MusicHubDbContext context, int producerId)
    {
        var albums = context.Albums
            .Where(a => a.ProducerId == producerId)
            .Select(a => new
            {
                a.Name,
                a.ReleaseDate,
                ProducerName = a.Producer != null ? a.Producer.Name : "",
                Songs = a.Songs
                    .Select(s => new
                    {
                        SongName = s.Name,
                        SongPrice = s.Price,
                        SongWriter = s.Writer.Name
                    })
                    .OrderByDescending(s => s.SongName)
                    .ThenBy(s => s.SongWriter)
                    .ToList(),
                TotalPrice = a.Price,
            })
            .ToList();

        albums = albums.OrderByDescending(a => a.TotalPrice).ToList();

        StringBuilder sb = new();
        foreach (var a in albums)
        {
            sb
                .AppendLine($"-AlbumName: {a.Name}")
                .AppendLine($"-ReleaseDate: {a.ReleaseDate.ToString("MM/dd/yyyy", CultureInfo.InvariantCulture)}")
                .AppendLine($"-ProducerName: {a.ProducerName}")
                .AppendLine("-Songs:");

            uint i = 1;
            foreach (var s in a.Songs)
            {
                sb.AppendLine($"---#{i}");
                sb.AppendLine($"---SongName: {s.SongName}");
                sb.AppendLine($"---Price: {s.SongPrice:F2}");
                sb.AppendLine($"---Writer: {s.SongWriter}");

                i++;
            }

            sb.AppendLine($"-AlbumPrice: {a.TotalPrice:F2}");
        }

        return sb.ToString().Trim();
    }
}