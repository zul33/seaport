using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaportClientApplication.Models
{
    public class ShipListModel
    {
        public List<Ship> Ships { get; set; }
        public string ResponseMessage { get; set; }
    }
}