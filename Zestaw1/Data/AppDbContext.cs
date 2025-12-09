// David Kezi Setondo 15634
using Microsoft.EntityFrameworkCore;
using Zestaw1.Models;

namespace Zestaw1.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options)
        {
        }

        public DbSet<Panel> Panels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Panel>().HasData(
                new Panel
                {
                    Id = 1,
                    Length = 100,
                    Width = 50,
                    LengthUnit = UnitType.cm,
                    WidthUnit = UnitType.cm,
                    Color = WoodColor.Dab,
                    HasBorder = true
                },
                new Panel
                {
                    Id = 2,
                    Length = 800,
                    Width = 600,
                    LengthUnit = UnitType.mm,
                    WidthUnit = UnitType.mm,
                    Color = WoodColor.Sosna,
                    HasBorder = false
                }
            );
        }
    }
}