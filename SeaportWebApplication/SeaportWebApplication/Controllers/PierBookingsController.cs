using System.Linq;
using System.Web.Http;
using SeaportWebApplication.Data;
using SeaportWebApplication.Models;
using SeaportWebApplication.Controllers.ActionResults;
using System;
using System.Web.Http.Description;
using System.Collections.Generic;

namespace SeaportWebApplication.Controllers
{
    public class PierBookingsController : ApiController
    {
        private SeaportContext db = new SeaportContext();

        // GET: api/PierBookings
        public IQueryable<PierBooking> GetPierBookings()
        {
            return db.PierBookings;
        }

        // POST: api/PierBookings
        [ResponseType(typeof(PierBooking))]
        public IHttpActionResult PostBooking(PierBooking pierBooking)
        {
            if(pierBooking == null)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, string.Format("Formatierung des Parameters konnte nicht bearbeitet werden."));
            }
            // Check if the date values ar valid
            if(pierBooking.BookedFrom >= pierBooking.BookedTo)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, string.Format("Das Anfangsdatum muss größer sein als das Enddatum."));
            }
            // Check if the pier and the ship exists
            Pier pier = db.Piers.Find(pierBooking.BookedShip.Id);
            Ship ship = db.Ships.Find(pierBooking.BookedPier.Id);
            if (pier != null && ship != null)
            {
                // check if the ship is active
                if (!ship.Active)
                {
                    return Content(System.Net.HttpStatusCode.BadRequest, string.Format("Das angegebene Schiff ({0}) wurde noch nicht hinzugefügt.", ship.Name));
                }
                // check if there is an overlapping with the dates of the current booking
                List<PierBooking> pierbookings = db.PierBookings.ToList();
                bool overlapExists = db.PierBookings.Any(pb => pb.BookedPier.Id == pier.Id && pb.BookedFrom <= pierBooking.BookedTo && pb.BookedTo >= pierBooking.BookedFrom);
                if(overlapExists)
                {
                    // check the other piers for Bookings
                    IQueryable<Pier> otherPiers = db.Piers.Where(p => !(p.PierBookings.Any(pb => pb.BookedFrom <= pierBooking.BookedTo && pb.BookedTo >= pierBooking.BookedFrom)));
                    return new BookingOverlappingDateResult(otherPiers);
                }
                // save the booking in the db
                PierBooking booking = new PierBooking
                {
                    BookedFrom = pierBooking.BookedFrom,
                    BookedTo = pierBooking.BookedTo,
                    BookedPier = pier,
                    BookedShip = ship
                };
                db.PierBookings.Add(booking);
                db.SaveChanges();
                return Ok("Buchung wurde erfolgreich angelegt.");
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