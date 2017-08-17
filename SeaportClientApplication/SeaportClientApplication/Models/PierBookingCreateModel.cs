using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SeaportClientApplication.Models
{
    public class PierBookingCreateModel
    {
        public int Id { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }

        public int BookedPierId { get; set; }

        public int BookedShipId { get; set; }
    }
}