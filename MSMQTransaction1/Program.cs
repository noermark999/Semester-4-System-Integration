using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Messaging;

namespace MSMQTransaction1
{
    class Program
    {
        static void Main(string[] args)
        {
            MessageQueueTransaction msgTx = new MessageQueueTransaction();
            MessageQueue msgQ = new MessageQueue(@".\private$\Orders");
            msgTx.Begin();
            try
            {
                msgQ.Send("First Message", msgTx);
                //Environment.Exit(1);
                msgQ.Send("Second Message", msgTx);
                msgTx.Commit();
            }
            catch
            {
                msgTx.Abort();
            }
            finally
            {
                msgQ.Close();
            }
        }
    }
}
