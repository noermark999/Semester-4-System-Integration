using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek18_Miniopgave
{
    internal class AICContentFilter
    {
        public XmlDocument Document { get; set; }
        public XmlDocument oldDocument { get; set; }
        public AICContentFilter(XmlDocument oldDocument)
        {
            Document = new XmlDocument();
            XmlElement newDocRoot = Document.CreateElement("root");
            Document.AppendChild(newDocRoot);

            this.oldDocument = oldDocument;
        }
        private void sendMessage()
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirportInformationCenter"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirportInformationCenter");
                messageQueue.Label = "CheckIn Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirportInformationCenter");
                messageQueue = new MessageQueue(@".\Private$\AirportInformationCenter");
                messageQueue.Label = "Newly Created Queue";
            }
            Message message = new Message();
            message.Body = Document;
            messageQueue.Send(message);
        }

        public void filterAndSend()
        {
            XmlNode root = Document.SelectSingleNode("//root");
            XmlNode oldNode = oldDocument.SelectSingleNode("//root");

            XmlNode cityNode = oldNode.SelectSingleNode("//city");
            XmlNode copiedCityNode = Document.ImportNode(cityNode, true);
            root.AppendChild(copiedCityNode);
            XmlNode coordNode = copiedCityNode.SelectSingleNode("//coord");
            copiedCityNode.RemoveChild(coordNode);

            XmlNode tempNode = oldNode.SelectSingleNode("//temperature");
            XmlNode copiedTempNode = Document.ImportNode(tempNode, true);
            root.AppendChild(copiedTempNode);

            sendMessage();
        }
    }
}
