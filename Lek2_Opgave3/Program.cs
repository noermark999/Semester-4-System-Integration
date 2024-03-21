using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;


namespace Lek2_Opgave3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            messageQueue = new MessageQueue(@".\Private$\TestQueue");
            messageQueue.Formatter = new XmlMessageFormatter(new Type[] { typeof(string) });
            var message = messageQueue.Receive();
            Console.WriteLine(message.Body);
            Console.ReadLine();
        }
    }
}
