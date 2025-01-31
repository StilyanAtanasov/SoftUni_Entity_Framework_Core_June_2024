using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data.Config;

public class TeamConfig : IEntityTypeConfiguration<Team>
{
    public void Configure(EntityTypeBuilder<Team> builder)
    {
        builder
            .HasOne(t => t.PrimaryKitColor)
            .WithMany(c => c.PrimaryKitTeams)
            .HasForeignKey(t => t.PrimaryKitColorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(t => t.SecondaryKitColor)
            .WithMany(c => c.SecondaryKitTeams)
            .HasForeignKey(t => t.SecondaryKitColorId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(t => t.HomeGames)
            .WithOne(g => g.HomeTeam)
            .HasForeignKey(g => g.HomeTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasMany(t => t.AwayGames)
            .WithOne(g => g.AwayTeam)
            .HasForeignKey(g => g.AwayTeamId)
            .OnDelete(DeleteBehavior.NoAction);

        builder
            .HasOne(t => t.Town)
            .WithMany(tn => tn.Teams)
            .HasForeignKey(t => t.TownId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}