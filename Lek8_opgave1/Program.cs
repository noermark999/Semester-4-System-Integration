using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace Lek8_opgave1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue msRequest = new MessageQueue(@".\Private$\FlightETARequest");
            MessageQueue msReply = new MessageQueue(@".\Private$\FlightETAReply");
           

            Request request = new Request(msReply, msRequest);
            Reply reply = new Reply(msRequest, msReply);

            request.sendRequest("ETA", "KL0125");
            reply.receiveRequest();
            request.receiveReply();   
            
            Console.ReadLine();

        }
    }
}
