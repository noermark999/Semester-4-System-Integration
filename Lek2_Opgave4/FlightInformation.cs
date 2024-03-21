using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek2_Opgave4
{
    internal class FlightInformation
    {
        public string FlySelskab { get; set; }
        public DateTime Afgang { get; set; }
        public DateTime Ankomst { get; set; }
        public string FlightNo { get; set; }
        public string Destination { get; set; }
        public DateTime CheckIn { get; set; }

        public FlightInformation(string flySelskab, DateTime afgang, DateTime ankomst, string flightNo, string destination, DateTime checkIn)
        {
            FlySelskab = flySelskab;
            Afgang = afgang;
            Ankomst = ankomst;
            FlightNo = flightNo;
            Destination = destination;
            CheckIn = checkIn;
        }
    }
}
