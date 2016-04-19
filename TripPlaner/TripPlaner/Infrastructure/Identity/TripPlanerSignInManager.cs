using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TripPlaner.DAL.Entities;

namespace TripPlaner.Infrastructure.Identity
{
    // Configure the application sign-in manager which is used in this application.
    public class TripPlanerSignInManager : SignInManager<Traveler, string>
    {
        public TripPlanerSignInManager(TripPlanerUserManager userManager, IAuthenticationManager authenticationManager)
            : base(userManager, authenticationManager)
        {
        }

        public override Task<ClaimsIdentity> CreateUserIdentityAsync(Traveler user)
        {
            return user.GenerateUserIdentityAsync((TripPlanerUserManager)UserManager);
        }

        public static TripPlanerSignInManager Create(IdentityFactoryOptions<TripPlanerSignInManager> options, IOwinContext context)
        {
            return new TripPlanerSignInManager(context.GetUserManager<TripPlanerUserManager>(), context.Authentication);
        }
    }
}