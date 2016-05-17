using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using TripPlaner.DAL;
using TripPlaner.DAL.Entities;

namespace TripPlaner.Controllers.ODataControllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using TripPlaner.DAL.Entities;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Traveler>("Travelers");
    builder.EntitySet<IdentityUserClaim>("IdentityUserClaims"); 
    builder.EntitySet<IdentityUserLogin>("IdentityUserLogins"); 
    builder.EntitySet<IdentityUserRole>("IdentityUserRoles"); 
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class TravelersController : ODataController
    {
        private TripPlanerDbContext db = new TripPlanerDbContext();

        // GET: odata/Travelers
        [EnableQuery]
        public IQueryable<Traveler> GetTravelers()
        {
            return db.Users;
        }

        // GET: odata/Travelers(5)
        [EnableQuery]
        public SingleResult<Traveler> GetTraveler([FromODataUri] string key)
        {
            return SingleResult.Create(db.Users.Where(traveler => traveler.Id == key));
        }

        // PUT: odata/Travelers(5)
        public async Task<IHttpActionResult> Put([FromODataUri] string key, Delta<Traveler> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Traveler traveler = await db.Users.FindAsync(key);
            if (traveler == null)
            {
                return NotFound();
            }

            patch.Put(traveler);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(traveler);
        }

        // POST: odata/Travelers
        public async Task<IHttpActionResult> Post(Traveler traveler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Users.Add(traveler);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TravelerExists(traveler.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return Created(traveler);
        }

        // PATCH: odata/Travelers(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public async Task<IHttpActionResult> Patch([FromODataUri] string key, Delta<Traveler> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Traveler traveler = await db.Users.FindAsync(key);
            if (traveler == null)
            {
                return NotFound();
            }

            patch.Patch(traveler);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TravelerExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(traveler);
        }

        // DELETE: odata/Travelers(5)
        public async Task<IHttpActionResult> Delete([FromODataUri] string key)
        {
            Traveler traveler = await db.Users.FindAsync(key);
            if (traveler == null)
            {
                return NotFound();
            }

            db.Users.Remove(traveler);
            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // GET: odata/Travelers(5)/Claims
        [EnableQuery]
        public IQueryable<IdentityUserClaim> GetClaims([FromODataUri] string key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.Claims);
        }

        // GET: odata/Travelers(5)/Logins
        [EnableQuery]
        public IQueryable<IdentityUserLogin> GetLogins([FromODataUri] string key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.Logins);
        }

        // GET: odata/Travelers(5)/Roles
        [EnableQuery]
        public IQueryable<IdentityUserRole> GetRoles([FromODataUri] string key)
        {
            return db.Users.Where(m => m.Id == key).SelectMany(m => m.Roles);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TravelerExists(string key)
        {
            return db.Users.Count(e => e.Id == key) > 0;
        }
    }
}
