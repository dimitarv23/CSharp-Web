namespace Cars.Infrastructure.Data
{
    using Cars.Infrastructure.Enums;
    using Cars.Infrastructure.Models;
    using Microsoft.EntityFrameworkCore;

    public class CarsDbContext : DbContext
    {
        public CarsDbContext(DbContextOptions<CarsDbContext> options)
            : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }

        public DbSet<Engine> Engines { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Car>()
                .Property(c => c.Type)
                .HasConversion(t => t.ToString(),
                    t => (eCarType)Enum.Parse(typeof(eCarType), t));

            modelBuilder.Entity<Engine>()
                .Property(e => e.Fuel)
                .HasConversion(f => f.ToString(),
                    f => (eFuel)Enum.Parse(typeof(eFuel), f));

            base.OnModelCreating(modelBuilder);
        }
    }
}