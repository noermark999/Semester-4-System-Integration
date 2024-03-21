using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace Lek9_Opgave3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Adapter adapter = new Adapter();
            MessageQueue msExcel = new MessageQueue(@".\Private$\Lek9_Opg3_Excel");
            bool done = false;

            while (!done)
            {
                Console.WriteLine("Flight number");
                string flightNumber = Console.ReadLine();
                Console.WriteLine("Year");
                int year = int.Parse(Console.ReadLine());
                Console.WriteLine("Month");
                int month = int.Parse(Console.ReadLine());
                Console.WriteLine("Day");
                int day = int.Parse(Console.ReadLine());
                Console.WriteLine("Hour");
                int hour = int.Parse(Console.ReadLine());
                Console.WriteLine("min");
                int min = int.Parse(Console.ReadLine());
                Console.WriteLine("sec");
                int sec = int.Parse(Console.ReadLine());
                Message message = new Message();

                DateTime testTime = new DateTime(year, month, day, hour, min, sec);

                message.Body = flightNumber + ";" + testTime.ToString();

                msExcel.Send(message);
                adapter.receiveMessage();
                Console.WriteLine("Done?");
                string doneAnswer = Console.ReadLine();
                if (doneAnswer == "yes")
                {
                    done = true;
                }
            }
        }
    }
}
