using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;
using System.Text.Json;

namespace Lek8_opgave1
{
    internal class Request
    {
        private MessageQueue inQueue;
        private MessageQueue outQueue;

        public Request(MessageQueue inQueue, MessageQueue outQueue)
        {
            this.inQueue = inQueue;
            this.outQueue = outQueue;
        }

        public void sendRequest(string besked, string flightnumber)
        {
            string json = JsonSerializer.Serialize(besked);
            Message message = new Message();
            message.Body = json;
            message.Label = flightnumber;
            message.ResponseQueue = inQueue;
            outQueue.Send(message);
            Console.WriteLine("Sends request from: " + flightnumber + ", ID: " + message.Id);
        }

        public void receiveReply()
        {
            Message message = inQueue.Receive();
            //Console.WriteLine("Received reply: " + message.Body);
        }
    }
}
