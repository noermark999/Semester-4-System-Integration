using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek16_Opgave2
{
    public class Passenger
    {
        public string TicketNo { get; set; }
        public string Name { get; set; }
        public string PassportNo { get; set; }
        public string FlightNo { get; set; }

        public Passenger(string ticketNo, string name, string passportNo, string flightNo)
        {
            TicketNo = ticketNo;
            Name = name;
            PassportNo = passportNo;
            FlightNo = flightNo;
        }

        public override string ToString()
        {
            return TicketNo + " " + Name + " " + PassportNo + " " + FlightNo;
        }
    }
}
