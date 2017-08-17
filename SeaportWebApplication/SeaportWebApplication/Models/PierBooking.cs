using System;

namespace SeaportWebApplication.Models
{
    public class PierBooking
    {
        public int Id { get; set; }

        public DateTime BookedFrom { get; set; }

        public DateTime BookedTo { get; set; }

        public virtual Pier BookedPier { get; set; }

        public virtual Ship BookedShip { get; set; }
    }
}