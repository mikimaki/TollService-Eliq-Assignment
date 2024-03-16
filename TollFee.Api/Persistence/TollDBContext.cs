#nullable disable

namespace TollFee.Api.Persistence
{
    using Microsoft.EntityFrameworkCore;

    public class TollDBContext : DbContext
    {
        public TollDBContext()
        {
        }

        public TollDBContext(DbContextOptions<TollDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<TollFeeRate> FeeRates { get; set; }
        public virtual DbSet<TollFeeZeroRate> ZeroFeeRates { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=TollDB;Trusted_Connection=True;");
                optionsBuilder.UseSqlServer("Server=localhost;initial catalog=TollDB;User Id=sa;Password=VippsPw1;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TollFeeRate>(entity => { entity.HasNoKey(); });

            modelBuilder.Entity<TollFeeZeroRate>(entity => { entity.HasNoKey(); });
        }

    }
}