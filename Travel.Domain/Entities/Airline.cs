using System;

namespace Travel.Domain
{
    public class Airline
    {
        public int id { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public string iata { get; set; }
        public string icao { get; set; }
        public string calls { get; set; }
        public string country { get; set; }
    }
}
