using System.Collections.Generic;

namespace SeaportClientApplication.Models
{
    public class Pier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<PierBooking> PierBookings { get; set; }
    }
}