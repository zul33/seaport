using System.Linq;
using System.Web.Http;
using System.Web.Http.Description;
using SeaportWebApplication.Data;
using SeaportWebApplication.Models;
using System.Collections.Generic;

namespace SeaportWebApplication.Controllers
{
    public class ShipsController : ApiController
    {
        private SeaportContext db = new SeaportContext();

        // GET: api/Ships
        public List<Ship> GetShips()
        {
            return db.Ships.ToList();
        }

        // GET: api/Ships/5
        [ResponseType(typeof(Ship))]
        public IHttpActionResult GetShip(int id)
        {
            Ship ship = db.Ships.Find(id);
            if (ship == null)
            {
                return NotFound();
            }

            return Ok(ship);
        }

        // POST: api/Ships
        [ResponseType(typeof(Ship))]
        public IHttpActionResult PostActive(Ship ship)
        {
            Ship dbShip = db.Ships.Find(ship.Id);
            if (dbShip != null)
            {
                if (dbShip.Active)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, string.Format("Das angegebene Schiff ({0}) wurde schon hinzugefügt.", ship.Name));
                }
                dbShip.Active = true;
                db.SaveChanges();
                return Ok(string.Format("Das Schiff {0} wurde hinzugefügt.", dbShip.Name));
            }
            return NotFound();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}