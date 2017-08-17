using Newtonsoft.Json;
using SeaportClientApplication.Models;
using SeaportClientApplication.Rest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SeaportClientApplication.Controllers
{
    public class ShipsController : Controller
    {
        public async Task<ActionResult> Index()
        {
            HttpResponseMessage Res = await RestRequests.GetRequest("api/Ships");
            List<Ship> ships = GetShips(true, Res);
            return View(new ShipListModel { Ships = ships });
        }

        public async Task<ActionResult> InactiveList()
        {
            HttpResponseMessage Res = await RestRequests.GetRequest("api/Ships");

            return View(GetShips(false, Res));
        }

        public async Task<ActionResult> Activate(int id)
        {
            List<KeyValuePair<string, string>> pairs = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("id", string.Format("{0}", id))
            };
            var content = new FormUrlEncodedContent(pairs);
            HttpResponseMessage postRes = await RestRequests.PostRequest("api/Ships/PostActive", content);
            string response = postRes.Content.ReadAsAsync<string>().Result;

            HttpResponseMessage Res = await RestRequests.GetRequest("api/Ships");
            List<Ship> ships = GetShips(true, Res);
            return View("Index", new ShipListModel { Ships = ships, ResponseMessage = response });
        }

        public async Task<ActionResult> Details(int id)
        {
            Ship shipInfo = new Ship();

            HttpResponseMessage Res = await RestRequests.GetRequest(string.Format("api/Ships/{0}", id));

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var shipResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the ship list  
                shipInfo = JsonConvert.DeserializeObject<Ship>(shipResponse);

            }
            //returning the ship list to view  
            return View(shipInfo);
        }

        private List<Ship> GetShips(bool active, HttpResponseMessage Res)
        {
            List<Ship> shipInfo = new List<Ship>();

            //Checking the response is successful or not which is sent using HttpClient  
            if (Res.IsSuccessStatusCode)
            {
                //Storing the response details recieved from web api   
                var shipResponse = Res.Content.ReadAsStringAsync().Result;

                //Deserializing the response recieved from web api and storing into the ship list  
                shipInfo = JsonConvert.DeserializeObject<List<Ship>>(shipResponse);

            }
            //returning the ship list to view  
            List<Ship> ships = shipInfo.Where(s => s.Active == active).ToList();
            return ships;
        }
    }
}
