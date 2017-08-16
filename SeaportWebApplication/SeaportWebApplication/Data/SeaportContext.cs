using SeaportWebApplication.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace SeaportWebApplication.Data
{
    public class SeaportContext : DbContext
    {
        public SeaportContext() : base("SeaportDatabase")
        {
        }

        public DbSet<Pier> Piers { get; set; }
        public DbSet<PierBooking> PierBookings { get; set; }
        public DbSet<Ship> Ships { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}