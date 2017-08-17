using Newtonsoft.Json;
using SeaportClientApplication.Models;
using SeaportClientApplication.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SeaportClientApplication.Controllers
{
    public class PierBookingsController : Controller
    {
        // GET: PierBookings
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage Res = await RestRequests.GetRequest("api/PierBookings");
            List<PierBooking> pierBookingInfo = GetPierBookings(Res);

            return View(new PierBookingListModel { PierBookings = pierBookingInfo });
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "BookedFrom,BookedTo,BookedPierId,BookedShipId")] PierBookingCreateModel pierBookingCreateModel)
        {
            if (ModelState.IsValid)
            {
                PierBooking booking = new PierBooking
                {
                    BookedFrom = pierBookingCreateModel.BookedFrom,
                    BookedTo = pierBookingCreateModel.BookedTo,
                    BookedPier = new Pier { Id = pierBookingCreateModel.BookedPierId },
                    BookedShip = new Ship { Id = pierBookingCreateModel.BookedShipId }
                };
                
                HttpResponseMessage postRes = RestRequests.JsonPostRequest("api/PierBookings/PostBooking", booking);                
                string response = postRes.Content.ReadAsStringAsync().Result;

                HttpResponseMessage Res = await RestRequests.GetRequest("api/PierBookings");
                List<PierBooking> pierBookings = GetPierBookings(Res);
                return View("Index", new PierBookingListModel { PierBookings = pierBookings, ResponseMessage = response });
            }
            return View();
        }

        public List<PierBooking> GetPierBookings(HttpResponseMessage Res)
        {            
            List<PierBooking> pierBookingInfo = new List<PierBooking>();

            if (Res.IsSuccessStatusCode)
            {
                var response = Res.Content.ReadAsStringAsync().Result;
                pierBookingInfo = JsonConvert.DeserializeObject<List<PierBooking>>(response);
            }

            return pierBookingInfo;
        }
    }
}