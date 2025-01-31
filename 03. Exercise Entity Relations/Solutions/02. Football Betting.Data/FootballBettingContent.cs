using Microsoft.EntityFrameworkCore;
using P02_FootballBetting.Data.Config;
using P02_FootballBetting.Data.Models;

namespace P02_FootballBetting.Data;

public class FootballBettingContext : DbContext
{
    public FootballBettingContext(DbContextOptions<FootballBettingContext> options)
        : base(options) { }

    public DbSet<Team> Teams { get; set; } = null!;
    public DbSet<Color> Colors { get; set; } = null!;
    public DbSet<Town> Towns { get; set; } = null!;
    public DbSet<Country> Countries { get; set; } = null!;
    public DbSet<Player> Players { get; set; } = null!;
    public DbSet<Position> Positions { get; set; } = null!;
    public DbSet<PlayerStatistic> PlayersStatistics { get; set; } = null!;
    public DbSet<Game> Games { get; set; } = null!;
    public DbSet<Bet> Bets { get; set; } = null!;
    public DbSet<User> Users { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PlayerStatisticConfig());

        base.OnModelCreating(modelBuilder);
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
            optionsBuilder.UseSqlServer("Server=.;Database=FootballBetting;Trusted_Connection=True;TrustServerCertificate=True;");
    }
}