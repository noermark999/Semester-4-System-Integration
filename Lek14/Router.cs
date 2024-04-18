using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek14
{
    internal class Router
    {
        protected MessageQueue inQueue;
        private List<string> list;

        public Router(MessageQueue inQueue)
        {
            this.inQueue = inQueue;

            inQueue.ReceiveCompleted += new ReceiveCompletedEventHandler(OnMessage);
            inQueue.BeginReceive();
            string label = inQueue.Label;
            list = new List<string>();
            list.Add("DK");
            list.Add("US");
            list.Add("UK");

        }
        private void OnMessage(Object source, ReceiveCompletedEventArgs asyncResult)
        {
            MessageQueue mq = (MessageQueue)source;
            Message message = mq.EndReceive(asyncResult.AsyncResult);
            string label = message.Label;
            XmlDocument xml = new XmlDocument();
            string XMLDocument = null;
            Console.WriteLine(label);
            Stream body = message.BodyStream;
            StreamReader reader = new StreamReader(body);
            XMLDocument = reader.ReadToEnd().ToString();
            xml.LoadXml(XMLDocument);
            XmlNodeList itemNodes = xml.SelectNodes("//Passport/Nationality");

            foreach (XmlNode itemNode in itemNodes)
            {
                foreach (string nat in list)
                {
                    if (itemNode.InnerText.Equals(nat))
                    {
                        Message passengerMessage = new Message();
                        passengerMessage.Body = itemNode.ParentNode;
                        passengerMessage.Label = xml.SelectSingleNode("//FirstName").InnerText + ", " + nat;

                        MessageQueue messageQueue = new MessageQueue(@".\Private$\" + "MQ" + nat);
                        messageQueue.Send(passengerMessage);
                        Console.WriteLine("Besked sendt til " + nat);
                    }
                }
            }
            mq.BeginReceive();
        }
    }
}
