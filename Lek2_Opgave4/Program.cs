using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Text.Json;

namespace Lek2_Opgave4
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            if (!MessageQueue.Exists(@".\Private$\Lek2_Opgav4")) {
                MessageQueue.Create(@".\Private$\Lek2_Opgav4");
            }
            messageQueue = new MessageQueue(@".\Private$\Lek2_Opgav4");

            FlightInformation flightInformation = new FlightInformation("SAS", new DateTime(2024, 02, 10, 14, 00, 00), new DateTime(2024, 02, 10, 20, 30, 00), "AB738491", "Hong Kong", new DateTime(2024, 02, 10, 12, 00, 00));
            string json = JsonSerializer.Serialize(flightInformation);
            Console.WriteLine(json);

            messageQueue.Send(json);

            Console.ReadLine();
        }
    }
}
