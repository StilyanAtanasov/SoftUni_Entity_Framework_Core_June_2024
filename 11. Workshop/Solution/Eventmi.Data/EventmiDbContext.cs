using Eventmi.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Eventmi.Data;

public class EventmiDbContext : DbContext
{
    public EventmiDbContext() { }

    public EventmiDbContext(DbContextOptions options) : base(options) { }

    public DbSet<Event> Events { get; set; } = null!;

    public DbSet<Town> Towns { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}