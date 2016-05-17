using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Microsoft.AspNet.Identity.EntityFramework;
using TripPlaner.DAL.Entities;

namespace TripPlaner.DAL
{
    public class TripPlanerDbContext : IdentityDbContext<Traveler>
    {
        private static readonly string _connectionString = "TripPlanerConnection";

        public TripPlanerDbContext() : base(_connectionString)
        {
        }

        public static TripPlanerDbContext Create()
        {
            return new TripPlanerDbContext();
        }

        public DbSet<Travel> Travels { get; set; }
        
        public DbSet<Placemark> Placemarks { get; set; }
        
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
    }
}
