using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MusicHub.Data.Models;

namespace MusicHub.Data.Config;

public class SongPerformerConfig : IEntityTypeConfiguration<SongPerformer>
{
    public void Configure(EntityTypeBuilder<SongPerformer> builder)
    {
        builder
            .HasKey(sp => new { sp.SongId, sp.PerformerId });
    }
}