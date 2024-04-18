using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Lek16_Opgave2;
using Lek16_Opgave2del2;

namespace Lek16_Opgave2del2
{
    internal class Program
    {
        static void Main(string[] args)
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

            List<Message> messages = new List<Message>();

            Consumer consumer = new Consumer(messageQueue, messages);
            while (true) { }
        }

        internal class Consumer
        {
            protected MessageQueue inQueue;
            protected List<Message> messages;
            public Consumer(MessageQueue inQueue, List<Message> messages)
            {
                this.inQueue = inQueue;
                this.messages = messages;
                inQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessage);
                inQueue.BeginReceive();
                string label = inQueue.Label;

            }
            private void OnMessage(Object source, ReceiveCompletedEventArgs asyncResult)
            {
                MessageQueue messageQueue = (MessageQueue)source;
                Message message = messageQueue.EndReceive(asyncResult.AsyncResult);
                messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
                messages.Add(message);
                message.Formatter = new XmlMessageFormatter(new Type[] { typeof(String) });
                Passenger passenger = JsonSerializer.Deserialize<Passenger>(message.Body.ToString());
                Console.WriteLine(passenger.ToString());
                inQueue.BeginReceive();
            }
        }
    }
}
