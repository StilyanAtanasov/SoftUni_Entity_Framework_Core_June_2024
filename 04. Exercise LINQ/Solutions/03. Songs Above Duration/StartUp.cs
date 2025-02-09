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

        Console.WriteLine(ExportSongsAboveDuration(context, 4));
    }

    public static string ExportSongsAboveDuration(MusicHubDbContext context, int duration)
    {
        var songs = context.Songs
            .Select(s => new
            {
                s.Name,
                WriterName = s.Writer.Name,
                SongPerformers = s.SongPerformers
                    .Select(sp => $"{sp.Performer.FirstName} {sp.Performer.LastName}")
                    .ToArray(),
                AlbumProducer = s.Album != null ? s.Album.Producer != null ? s.Album.Producer.Name : "" : "",
                s.Duration
            })
            .OrderBy(s => s.Name)
            .ThenBy(s => s.WriterName)
            .ToList();

        songs = songs.Where(s => s.Duration.Ticks > TimeSpan.FromSeconds(duration).Ticks).ToList();

        StringBuilder sb = new();
        uint i = 1;

        foreach (var s in songs)
        {
            sb.AppendLine($"-Song #{i}");
            sb.AppendLine($"---SongName: {s.Name}");
            sb.AppendLine($"---Writer: {s.WriterName}");

            foreach (string p in s.SongPerformers.OrderBy(p => p)) sb.AppendLine($"---Performer: {p}");

            sb.AppendLine($"---AlbumProducer: {s.AlbumProducer}");
            sb.AppendLine($"---Duration: {s.Duration.ToString("c")}");

            i++;
        }

        return sb.ToString().Trim();
    }
}