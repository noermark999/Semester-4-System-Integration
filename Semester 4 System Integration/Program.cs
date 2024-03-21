using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Lek2_Opgave2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue messageQueue = null;
            MessageQueue.Create(@".\Private$\TestQueue");
            messageQueue = new MessageQueue(@".\Private$\TestQueue");
            messageQueue.Send("Besked sendt til MSMQ", "Titel");
        }
    }
}
