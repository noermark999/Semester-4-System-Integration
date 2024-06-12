using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lek18_Miniopgave
{
    internal class Program
    {
        
        static void Main(string[] args)
        {
            OriginalData originalData = new OriginalData("Copenhagen");
            Console.WriteLine(originalData.Document.OuterXml);
            RecipientList recipient = new RecipientList(originalData.Document);
            recipient.sendMessages();
            Console.ReadLine();
        }
    }
}
