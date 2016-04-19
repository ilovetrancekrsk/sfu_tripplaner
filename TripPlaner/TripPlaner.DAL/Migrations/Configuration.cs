using TripPlaner.DAL.Entities;

namespace TripPlaner.DAL.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<TripPlaner.DAL.TripPlanerDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TripPlaner.DAL.TripPlanerDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            context.Placemarks.AddOrUpdate(new Placemark
            {
                Arrival = DateTime.Now.AddDays(4),
                Departure = DateTime.Now.AddDays(3),
                Name = "Point 1",
                Description = "Description of Point 1",
                IsVisited = false,
                Latitude = "123123.33",
                Longitude = "78654.23",
                Type = PlacemarkTypes.Attraction
            });

            context.Placemarks.AddOrUpdate(new Placemark
            {
                Arrival = DateTime.Now.AddDays(14),
                Departure = DateTime.Now.AddDays(11),
                Name = "Point 2",
                Description = "Description of Point 2",
                IsVisited = false,
                Latitude = "2343123.33",
                Longitude = "786654.23",
                Type = PlacemarkTypes.Attraction
            });
        }
    }
}
