using System;

namespace SeaportWebApplication.Models
{
    public class PierBooking
    {
        public int Id { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }

        public Pier BookedPier { get; set; }

        public Ship BookedShip { get; set; }
    }
}