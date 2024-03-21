using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Lek9_Opg1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            MessageQueue msResponsTimeTest = new MessageQueue(@".\Private$\Lek09_Opg1");

            Message message = new Message();
            message.Body = "Test responstid";
            message.Label = "Test responstid";
            message.TimeToBeReceived = new TimeSpan(0, 0, 15);
            msResponsTimeTest.Send(message);
            //Virker, den bliver slettet efter 15 sekunder :)
        }
    }
}
