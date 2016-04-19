using Microsoft.AspNet.Identity.EntityFramework;
using TripPlaner.DAL;
using TripPlaner.DAL.Entities;

namespace TripPlaner.Infrastructure.Identity
{
    public class TripPlanerUserStore : UserStore<Traveler>
    {
        public TripPlanerUserStore(TripPlanerDbContext context)
            : base(context)
        {
            
        }
    }
}