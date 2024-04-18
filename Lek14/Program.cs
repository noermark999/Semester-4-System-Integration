using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek14
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> list = new List<string>();
            list.Add("DK");
            list.Add("US");
            list.Add("UK");
            XmlDocument xmlDocTest1 = new XmlDocument();
            xmlDocTest1.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<CBPArrivalInfo>\r\n<Flight number=\"SK937\" Flightdate=\"20170225\">\r\n<Origin>ARLANDA</Origin>\r\n<Destination>Bluff City International</Destination>\r\n</Flight>\r\n<Passenger>\r\n<FirstName>Anders</FirstName>\r\n<LastName>And</LastName>\r\n<DayOfBirth>June 9th 1934</DayOfBirth>\r\n<Height>2’9”</Height>\r\n<Sex>D</Sex>\r\n</Passenger>\r\n<Passport>\r\n<PassNo>200305252</PassNo>\r\n<Nationality>DK</Nationality>\r\n</Passport>\r\n</CBPArrivalInfo>");
            XmlDocument xmlDocTest2 = new XmlDocument();
            xmlDocTest2.LoadXml("<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<CBPArrivalInfo>\r\n<Flight number=\"SK937\" Flightdate=\"20170225\">\r\n<Origin>ARLANDA</Origin>\r\n<Destination>Bluff City International</Destination>\r\n</Flight>\r\n<Passenger>\r\n<FirstName>Andersine</FirstName>\r\n<LastName>And</LastName>\r\n<DayOfBirth>June 9th 1934</DayOfBirth>\r\n<Height>2’9”</Height>\r\n<Sex>D</Sex>\r\n</Passenger>\r\n<Passport>\r\n<PassNo>200305252</PassNo>\r\n<Nationality>DK</Nationality>\r\n</Passport>\r\n<Passport>\r\n<PassNo>326725295</PassNo>\r\n<Nationality>US</Nationality>\r\n</Passport>\r\n</CBPArrivalInfo>");

            MessageQueue routerQueue = new MessageQueue(@".\Private$\lek14Router");
            Router router = new Router(routerQueue);

            Message messageTest1 = new Message();
            messageTest1.Body = xmlDocTest1;
            routerQueue.Send(messageTest1);

            Message messageTest2 = new Message();
            messageTest2.Body = xmlDocTest2;
            routerQueue.Send(messageTest2);

            Console.ReadLine();
        }
    }
}
