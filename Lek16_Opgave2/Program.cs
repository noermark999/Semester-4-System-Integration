using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Lek16_Opgave2
{
    internal class Program
    {
        static void Main(string[] args)
        {

            //Programmet virker ikke da jeg bruger json og det fucker op med message formatter. lav det om så den bruger xml istedet

            while (true)
            {
                Console.WriteLine("Indtast ticketno");
                string ticketno = Console.ReadLine();
                Console.WriteLine("Indtast Name");
                string name = Console.ReadLine();
                Console.WriteLine("Indtast Passportno");
                string passportNo = Console.ReadLine();
                Console.WriteLine("Indtast flightno");
                string flightNo = Console.ReadLine();
                checkInEmployee.CheckInPassager(ticketno, name, passportNo, flightNo);
            }
        }
        class checkInEmployee
        {
            public static void CheckInPassager(string TicketNo, string Name, string PassportNo, string FlightNo)
            {
                MessageQueue messageQueue = null;
                if (MessageQueue.Exists(@".\Private$\Lek16_checkInEmployee"))
                {
                    messageQueue = new MessageQueue(@".\Private$\Lek16_checkInEmployee");
                    messageQueue.Label = "CheckIn Queue";
                }
                else
                {
                    // Create the Queue
                    MessageQueue.Create(@".\Private$\Lek16_checkInEmployee");
                    messageQueue = new MessageQueue(@".\Private$\Lek16_checkInEmployee");
                    messageQueue.Label = "Newly Created Queue";
                }
                Message message = new Message();
                Passenger passenger = new Passenger(TicketNo, Name, PassportNo, FlightNo);
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                string json = JsonSerializer.Serialize<Passenger>(passenger);
                Console.WriteLine(json);
                message.Body = json;
                messageQueue.Send(message);
            }

        }
    }
}
