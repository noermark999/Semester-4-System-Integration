using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;

namespace Lek8_opgave1
{
    internal class Reply
    {
        private MessageQueue inQueue;
        private MessageQueue outQueue;

        public Reply(MessageQueue inQueue, MessageQueue outQueue)
        {
            this.inQueue = inQueue;
            this.outQueue = outQueue;
        }

        public void receiveRequest()
        {
            Message message = inQueue.Receive();
            //Console.WriteLine(message.Body);
            Console.WriteLine("Received request: " + ", from: ");
            DateTime eta = new DateTime(2024, 02, 27, 11, 35, 00);

            string info = eta.ToString() + ", " + message.Label;

            string json = JsonSerializer.Serialize(info);
            MessageQueue replyQueue = message.ResponseQueue;
            
            Message replyMessage = new Message();
            replyMessage.Body = json;
            replyMessage.Label = "ETA";
            replyMessage.CorrelationId = message.Id;

            replyQueue.Send(replyMessage);
            Console.WriteLine("Sends reply with info: " + json + ", from id: " + replyMessage.CorrelationId);
        }
    }
}
