using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Lek18_Miniopgave
{
    internal class ACContentFilter
    {
        public XmlDocument Document { get; set; }
        public XmlDocument oldDocument { get; set; }
        public ACContentFilter(XmlDocument oldDocument)
        {
            Document = new XmlDocument();
            XmlElement newDocRoot = Document.CreateElement("root");
            Document.AppendChild(newDocRoot);

            this.oldDocument = oldDocument;
        }
        private void sendMessage()
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirlineCompanies"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirlineCompanies");
                messageQueue.Label = "CheckIn Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirlineCompanies");
                messageQueue = new MessageQueue(@".\Private$\AirlineCompanies");
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
            XmlNode sunNode = copiedCityNode.SelectSingleNode("//sun");
            copiedCityNode.RemoveChild(sunNode);
            XmlNode coordNode = copiedCityNode.SelectSingleNode("//coord");
            copiedCityNode.RemoveChild(coordNode);

            XmlNode tempNode = oldNode.SelectSingleNode("//temperature");
            XmlNode copiedTempNode = Document.ImportNode(tempNode, true);
            root.AppendChild(copiedTempNode);

            // Håndter clouds node
            XmlNode cloudsNode = oldNode.SelectSingleNode("//clouds");
            XmlNode copiedCloudsNode = Document.ImportNode(cloudsNode, true);
            root.AppendChild(copiedCloudsNode);

            sendMessage();
        }
    }
}
