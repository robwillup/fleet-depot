using FleetDepot.Types.Models;
using Microsoft.EntityFrameworkCore;

namespace FleetDepot.Dal;

public class FleetDepotDb : DbContext
{
    public DbSet<Vehicle> Vehicles => Set<Vehicle>();

    public FleetDepotDb(DbContextOptions<FleetDepotDb> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseInMemoryDatabase("FleetDepotDb");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Vehicle>(vehicle =>
        {
            vehicle.HasKey(v => new { v.Series, v.Number });

            vehicle.Property(v => v.Type)
                .IsRequired().HasConversion<string>()
                .HasMaxLength(20);
        });
    }
}
