namespace SeaportWebApplication.Models
{
    public class Pier
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Ship Ship { get; set; }
    }
}