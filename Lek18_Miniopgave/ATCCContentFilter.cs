using System;
using System.Collections.Generic;
using System.Linq;
using System.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Lek18_Miniopgave
{
    internal class ATCCContentFilter
    {
        public XmlDocument Document { get; set; }
        public XmlDocument oldDocument { get; set; }
        public ATCCContentFilter(XmlDocument oldDocument) 
        {
            Document = new XmlDocument();
            XmlElement newDocRoot = Document.CreateElement("root");
            Document.AppendChild(newDocRoot);

            this.oldDocument = oldDocument;
        }
        private void sendMessage()
        {
            MessageQueue messageQueue = null;
            if (MessageQueue.Exists(@".\Private$\AirTrafficControlCenter"))
            {
                messageQueue = new MessageQueue(@".\Private$\AirTrafficControlCenter");
                messageQueue.Label = "CheckIn Queue";
            }
            else
            {
                // Create the Queue
                MessageQueue.Create(@".\Private$\AirTrafficControlCenter");
                messageQueue = new MessageQueue(@".\Private$\AirTrafficControlCenter");
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

            XmlNode tempNode = oldNode.SelectSingleNode("//temperature");
            XmlNode copiedTempNode = Document.ImportNode(tempNode, true);
            root.AppendChild(copiedTempNode);

            XmlNode humidNode = oldNode.SelectSingleNode("//humidity");
            XmlNode copiedHumidNode = Document.ImportNode(humidNode, true);
            root.AppendChild(copiedHumidNode);

            // Håndter pressure node
            XmlNode pressureNode = oldNode.SelectSingleNode("//pressure");
            XmlNode copiedPressureNode = Document.ImportNode(pressureNode, true);
            root.AppendChild(copiedPressureNode);

            // Håndter wind node
            XmlNode windNode = oldNode.SelectSingleNode("//wind");
            XmlNode copiedWindNode = Document.ImportNode(windNode, true);
            root.AppendChild(copiedWindNode);

            // Håndter clouds node
            XmlNode cloudsNode = oldNode.SelectSingleNode("//clouds");
            XmlNode copiedCloudsNode = Document.ImportNode(cloudsNode, true);
            root.AppendChild(copiedCloudsNode);

            // Håndter visibility node
            XmlNode visiNode = oldNode.SelectSingleNode("//visibility");
            XmlNode copiedVisiNode = Document.ImportNode(visiNode, true);
            root.AppendChild(copiedVisiNode);

            sendMessage();
        }
    }
}
