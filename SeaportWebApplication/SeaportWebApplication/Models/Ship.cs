using System.Collections.Generic;

namespace SeaportWebApplication.Models
{
    public class Ship
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Active { get; set; }

        public ICollection<PierBooking> PierBookings { get; set; }

        public Ship()
        {
            Active = false;
        }
    }
}