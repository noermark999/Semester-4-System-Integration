using MYFirstMSMQ;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Lek16_Opgave1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportCheckInOutput"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInOutput");
                messageQueue.Label = "CheckIn Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportCheckInOutput");
                messageQueue = new MessageQueue(@".\Private$\AirportCheckInOutput");
                messageQueue.Label = "Newly Created Queue";
            }

            XElement CheckInFile = XElement.Load(@"CheckedInPassenger.xml");
            Console.WriteLine(CheckInFile);
            string AirlineCompany = "SAS";

            messageQueue.Send(CheckInFile, AirlineCompany);

            Splitter splitter = new Splitter(messageQueue);
            Console.ReadLine();
        }
    }
}
